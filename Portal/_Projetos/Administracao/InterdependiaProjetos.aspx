<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterdependiaProjetos.aspx.cs"
    Inherits="_Projetos_Administracao_InterdependiaProjetos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            width: 10px;
        }

        .style2 {
            width: 10px;
            height: 10px;
        }

        .style3 {
            height: 10px;
        }
                
        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%">
                <PanelCollection>
                    
                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div style="display: flex; justify-content: space-between;">
                                        <div style="padding: 0 10px; width: 50%;">
                                             <div id="divGrid" style="visibility:hidden">
                                            <dxwgv:ASPxGridView ID="gvProjetosDependenciaFilho" runat="server" AutoGenerateColumns="False"
                                                ClientInstanceName="gvProjetosDependenciaFilho" DataSourceID="dsProjetosDependenciaFilho"
                                                KeyFieldName="CodigoProjetoDependencia"
                                                OnCommandButtonInitialize="grid_CommandButtonInitialize" Width="100%">
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowInCustomizationForm="True"
                                                        VisibleIndex="0" Width="70px" ShowDeleteButton="true">
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
                                                                            <Paddings Padding="0px" />
                                                                            <Items>
                                                                                <dxm:MenuItem runat="server" Name="btnIncluir" Text="" ToolTip="Incluir">
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
                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoProjetoDependencia" ShowInCustomizationForm="True"
                                                        Visible="False" VisibleIndex="1">
                                                        <PropertiesComboBox DataSourceID="dsProjetosDisponiveisFilho" IncrementalFilteringMode="Contains"
                                                            TextField="NomeProjeto" ValueField="CodigoProjeto">
                                                            <ItemStyle Wrap="True" />
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Caption="Projeto" CaptionLocation="Top" Visible="True" VisibleIndex="0" />
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomeProjetoDependencia" ShowInCustomizationForm="True"
                                                        VisibleIndex="2">
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="TipoLink" ShowInCustomizationForm="True"
                                                        Visible="False" VisibleIndex="3">
                                                        <PropertiesComboBox DropDownRows="2">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Projeto - Projeto" Value="PJPJ" />
                                                                <dxe:ListEditItem Text="Projeto - Sub-projeto" Value="PJSP" />
                                                            </Items>
                                                            <ValidationSettings CausesValidation="True">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Caption="Tipo Dependência" CaptionLocation="Top" Visible="True"
                                                            VisibleIndex="1" />
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tipo Dependencia" FieldName="TipoDependencia"
                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                        <PropertiesTextEdit>
                                                            <ValidationSettings CausesValidation="True">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Visible="false" Width="90px">
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataImageColumn Caption="Desempenho" Visible="false" FieldName="Desempenho" ShowInCustomizationForm="True"
                                                        VisibleIndex="6" Width="90px">
                                                        <PropertiesImage ImageUrlFormatString="../../imagens/{0}.gif">
                                                        </PropertiesImage>
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataImageColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />

                                                <SettingsCommandButton>
                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                </SettingsCommandButton>

                                                <SettingsPopup>
                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                        AllowResize="True" Width="400px" />
                                                </SettingsPopup>
                                                <Settings ShowTitlePanel="True" VerticalScrollBarMode="Visible" />
                                                <SettingsText ConfirmDelete="Deseja excluir a dependência?" PopupEditFormCaption="Dependência de projetos" />
                                                <ClientSideEvents EndCallback="function(s, e) {
if(gvProjetosDependenciaFilho.cp_MensagemErro === '007')
			{

window.top.mostraMensagem(traducao.InterdependiaProjetos_n_o___poss_vel_associar_esse_projeto_como_Pai_pois_ele_j__est__associado_como_filho_, 'Atencao', true, false, null);
                                                                  gvProjetosDependenciaFilho.cp_MensagemErro = &quot;&quot;;
                                                }


	             }

                                                                                        
" Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
    document.getElementById('divGrid1').style.visibility = '';

}" />
                                                <Templates>
                                                    <TitlePanel>
                                                        <%# string.Format("Projetos/Programas que dependem de <b>'{0}' (Projeto Pai)</b>", nomeProjeto)%>
                                                    </TitlePanel>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                            </div>
                                                 <asp:SqlDataSource ID="dsProjetosDependenciaFilho" runat="server" DeleteCommand="DELETE FROM LinkProjeto WHERE CodigoProjetoPai = @CodigoProjetoDependencia AND CodigoProjetoFilho = @CodigoProjeto "
                                                InsertCommand="INSERT INTO LinkProjeto
           (CodigoProjetoPai
           ,CodigoProjetoFilho
           ,TipoLink
           ,PesoProjetoFilho
           ,AtualizaTarefaLink
           ,CodigoTarefaLink)
     VALUES
           (@CodigoProjetoDependencia
           ,@CodigoProjeto
           ,@TipoLink
           ,100
           ,'S'
           ,NULL)"
                                                SelectCommand="SELECT p.NomeProjeto AS NomeProjetoDependencia,
              lp.CodigoProjetoPai AS CodigoProjetoDependencia,
              lp.TipoLink,
             CASE lp.TipoLink
	WHEN 'PP' THEN 'Programa - Projeto'
	WHEN 'PJPJ' THEN 'Projeto - Projeto'
	WHEN 'PJSP' THEN 'Projeto - SubProjeto'
             END AS TipoDependencia,
             s.DescricaoStatus AS Status,
             RTRIM(LTRIM(rp.CorGeral)) AS Desempenho
 FROM LinkProjeto lp INNER JOIN
             Projeto p ON p.CodigoProjeto = lp.CodigoProjetoPai INNER JOIN
             Status s ON s.CodigoStatus = p.CodigoStatusProjeto INNER JOIN
             ResumoProjeto rp on rp.CodigoProjeto = p.CodigoProjeto
WHERE p.DataExclusao IS NULL
    AND lp.CodigoProjetoFilho = @CodigoProjeto
	ORDER BY
				p.NomeProjeto"
                                                OnInserted="dsProjetosDependenciaFilho_Inserted" OnSelected="dsProjetosDependenciaFilho_Selected" OnInserting="dsProjetosDependenciaFilho_Inserting">
                                                <DeleteParameters>
                                                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                    <asp:Parameter Name="CodigoProjetoDependencia" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                    <asp:Parameter Name="CodigoProjetoDependencia" />
                                                    <asp:Parameter Name="TipoLink" />
                                                </InsertParameters>
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="dsProjetosDisponiveisFilho" runat="server" SelectCommand=" SELECT p.CodigoProjeto,
				p.NomeProjeto
	 FROM Projeto p
	WHERE p.DataExclusao IS NULL
		AND p.CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ' OR tp.IndicaTipoProjeto = 'PRG')
		AND p.CodigoEntidade = (SELECT p1.CodigoEntidade FROM Projeto p1 WHERE p1.CodigoProjeto = @CodigoProjeto)
		AND NOT EXISTS (SELECT 1 FROM LinkProjeto lp WHERE lp.CodigoProjetoFilho = p.CodigoProjeto)
        AND (p.CodigoProjeto != @CodigoProjeto)
	ORDER BY
				p.NomeProjeto">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <dxwgv:ASPxGridViewExporter ID="gvExporterProjetosDependenciaFilho" runat="server"
                                                GridViewID="gvProjetosDependenciaFilho" OnRenderBrick="gvExporterProjetosDependenciaFilho_RenderBrick">
                                                <Styles>
                                                    <Default>
                                                    </Default>
                                                    <Header>
                                                    </Header>
                                                    <Cell>
                                                    </Cell>
                                                    <GroupFooter Font-Bold="True">
                                                    </GroupFooter>
                                                    <Title Font-Bold="True"></Title>
                                                </Styles>
                                            </dxwgv:ASPxGridViewExporter>
                                        </div>
                                        <div style="padding: 0 10px; width: 50%;">
                                            <div id="divGrid1" style="visibility:hidden">
                                            <dxwgv:ASPxGridView ID="gvProjetosDependenciaPai" runat="server" AutoGenerateColumns="False"
                                                ClientInstanceName="gvProjetosDependenciaPai" DataSourceID="dsProjetosDependenciaPai"
                                                Width="100%" KeyFieldName="CodigoProjetoDependencia" OnCommandButtonInitialize="grid_CommandButtonInitialize">
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowInCustomizationForm="True"
                                                        VisibleIndex="0" Width="70px" ShowDeleteButton="true">
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <dxm:ASPxMenu ID="menu2" runat="server" BackColor="Transparent" ClientInstanceName="menu2"
                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick1" OnInit="menu_Init1">
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
                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoProjetoDependencia" ShowInCustomizationForm="True"
                                                        Visible="False" VisibleIndex="1">
                                                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsProjetosDisponiveisPai"
                                                            IncrementalFilteringMode="Contains" TextField="NomeProjeto" ValueField="CodigoProjeto">
                                                            <ItemStyle Wrap="True" />
                                                            <ValidationSettings CausesValidation="True">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Caption="Projeto" CaptionLocation="Top" Visible="True" VisibleIndex="0" />
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomeProjetoDependencia" ShowInCustomizationForm="True"
                                                        VisibleIndex="2">
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="TipoLink" ShowInCustomizationForm="True"
                                                        Visible="False" VisibleIndex="3">
                                                        <PropertiesComboBox DropDownRows="2" ValueType="System.String">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Projeto - Projeto" Value="PJPJ" />
                                                                <dxe:ListEditItem Text="Projeto - Sub-projeto" Value="PJSP" />
                                                            </Items>
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Caption="Tipo Dependência" CaptionLocation="Top" Visible="True"
                                                            VisibleIndex="1" />
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tipo Dependencia" FieldName="TipoDependencia"
                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                        <PropertiesTextEdit>
                                                            <ValidationSettings CausesValidation="True">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Status" Visible="false" FieldName="Status" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Width="90px">
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataImageColumn Caption="Desempenho" Visible="false" FieldName="Desempenho" ShowInCustomizationForm="True"
                                                        VisibleIndex="6" Width="90px">
                                                        <PropertiesImage ImageUrlFormatString="../../imagens/{0}.gif">
                                                        </PropertiesImage>
                                                        <EditFormSettings Visible="False" />
                                                    </dxwgv:GridViewDataImageColumn>
                                                    <dxtv:GridViewDataSpinEditColumn Caption="Peso" Visible="false" FieldName="PesoProjetoFilho" ShowInCustomizationForm="False" VisibleIndex="7" Width="110px">
                                                        <PropertiesSpinEdit DisplayFormatString="g">
                                                            <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        <Settings AllowAutoFilter="False" />
                                                        <EditFormSettings Visible="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </dxtv:GridViewDataSpinEditColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />

                                                <SettingsCommandButton>
                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                </SettingsCommandButton>

                                                <SettingsPopup>
                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                        AllowResize="True" Width="400px" />
                                                </SettingsPopup>
                                                <Settings VerticalScrollBarMode="Visible" ShowTitlePanel="True" />
                                                <SettingsText ConfirmDelete="Deseja excluir a dependência:" PopupEditFormCaption="Dependência de projetos" />
                                                <ClientSideEvents EndCallback="function(s, e) {
                                                                                            if(gvProjetosDependenciaPai.cp_MensagemErro === '1')
                                                                                            {
                                                                                                                                        window.top.mostraMensagem(traducao.InterdependiaProjetos_n_o___poss_vel_associar_esse_projeto_como_filho_pois_ele_j__est__associado_como_pai_, 'Atencao', true, false, null);
                                                                                                                                        gvProjetosDependenciaPai.cp_MensagemErro = &quot;&quot;;
                                                                                            }
                                                                                       }

                                                                                         " Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
    document.getElementById('divGrid1').style.visibility = '';

}" />
                                                <Templates>
                                                    <TitlePanel>
                                                        <%# string.Format("Projetos/Programas dos quais <b>'{0}'</b> depende (Projetos Filho)", nomeProjeto) %>
                                                    </TitlePanel>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                            </div>
                                                <asp:SqlDataSource ID="dsProjetosDependenciaPai" runat="server" SelectCommand=" SELECT p.NomeProjeto AS NomeProjetoDependencia,
				lp.CodigoProjetoFilho AS CodigoProjetoDependencia,
				lp.TipoLink,
				CASE lp.TipoLink
					WHEN 'PP' THEN 'Programa - Projeto'
					WHEN 'PJPJ' THEN 'Projeto - Projeto'
					WHEN 'PJSP' THEN 'Projeto - SubProjeto'
				END AS TipoDependencia,
				s.DescricaoStatus AS Status,
				RTRIM(LTRIM(rp.CorGeral)) AS Desempenho,
                lp.PesoProjetoFilho
   FROM LinkProjeto lp INNER JOIN
				Projeto p ON p.CodigoProjeto = lp.CodigoProjetoFilho INNER JOIN
				Status s ON s.CodigoStatus = p.CodigoStatusProjeto INNER JOIN
				ResumoProjeto rp on rp.CodigoProjeto = p.CodigoProjeto
	WHERE p.DataExclusao IS NULL
		AND lp.CodigoProjetoPai = @CodigoProjeto
        AND (p.CodigoProjeto != @CodigoProjeto)
	ORDER BY
				p.NomeProjeto"
                                                DeleteCommand="DELETE FROM LinkProjeto WHERE CodigoProjetoPai = @CodigoProjeto AND CodigoProjetoFilho = @CodigoProjetoDependencia"
                                                InsertCommand="INSERT INTO LinkProjeto
           (CodigoProjetoPai
           ,CodigoProjetoFilho
           ,TipoLink
           ,PesoProjetoFilho
           ,AtualizaTarefaLink
           ,CodigoTarefaLink)
     VALUES
           (@CodigoProjeto
           ,@CodigoProjetoDependencia
           ,@TipoLink
           ,100
           ,'S'
           ,NULL)"
                                                OnInserted="dsProjetosDependenciaPai_Inserted" OnInserting="dsProjetosDependenciaPai_Inserting">
                                                <DeleteParameters>
                                                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                    <asp:Parameter Name="CodigoProjetoDependencia" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                    <asp:Parameter Name="CodigoProjetoDependencia" />
                                                    <asp:Parameter Name="TipoLink" />
                                                </InsertParameters>
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="dsProjetosDisponiveisPai" runat="server" SelectCommand=" SELECT p.CodigoProjeto,
				p.NomeProjeto
	 FROM Projeto p
	WHERE p.DataExclusao IS NULL
		AND p.CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ' OR tp.IndicaTipoProjeto = 'PRG')
		AND p.CodigoEntidade = (SELECT p1.CodigoEntidade FROM Projeto p1 WHERE p1.CodigoProjeto = @CodigoProjeto)
		AND NOT EXISTS (SELECT 1 FROM LinkProjeto lp WHERE lp.CodigoProjetoFilho = p.CodigoProjeto OR lp.CodigoProjetoPai = p.CodigoProjeto)
        AND (p.CodigoProjeto != @CodigoProjeto)
	ORDER BY
				p.NomeProjeto">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter DefaultValue="" Name="CodigoProjeto" QueryStringField="IDProjeto" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <dxwgv:ASPxGridViewExporter ID="gvExporterProjetosDependenciaPai" runat="server"
                                                GridViewID="gvProjetosDependenciaPai" OnRenderBrick="gvExporterProjetosDependenciaPai_RenderBrick">
                                                <Styles>
                                                    <Default>
                                                    </Default>
                                                    <Header>
                                                    </Header>
                                                    <Cell>
                                                    </Cell>
                                                    <GroupFooter Font-Bold="True">
                                                    </GroupFooter>
                                                    <Title Font-Bold="True"></Title>
                                                </Styles>
                                            </dxwgv:ASPxGridViewExporter>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </div>
    </form>
</body>
</html>
