<%@ Page MasterPageFile="~/novaCdis.master" Language="C#" AutoEventWireup="true"
    CodeFile="auditoria_Lista_New.aspx.cs" Inherits="administracao_auditoria_Lista_New" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 10px;
        }
        .style3
        {
            width: 10px;
            height: 5px;
        }
    </style>
    </head>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Auditoria">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="1" class="style1">
        <tr>
            <td class="style3">
            </td>
            <td>
                <table>
                    <tr>
                        <td style="padding-left: 5px">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Operação:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="padding-left: 5px">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="Tabela:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="padding-left: 5px">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Campo:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="padding-left: 5px">
                            <dxe:ASPxLabel ID="lblValorProcurar" runat="server" Font-Bold="False"
                                Text="Valor a pesquisar:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 5px; width: 100px">
                            <dxe:ASPxComboBox ID="ddlOperacao" runat="server" ClientInstanceName="ddlOperacao"
                                 SelectedIndex="0" Width="100%">
                                <Items>
                                    <dxe:ListEditItem Selected="True" Text="Atualização" Value="U" />
                                    <dxe:ListEditItem Text="Inclusão" Value="I" />
                                    <dxe:ListEditItem Text="Exclusão" Value="E" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="padding-left: 5px; width: 230px">
                            <dxe:ASPxComboBox ID="ddlTabela" runat="server" ClientInstanceName="ddlTabela"
                                 Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlCampos.PerformCallback();
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="padding-left: 5px; width: 230px">
                            <dxe:ASPxComboBox ID="ddlCampos" runat="server" ClientInstanceName="ddlCampos"
                                SelectedIndex="0" Width="100%">
                                <Items>
                                    <dxe:ListEditItem Selected="True" Text="Atualização" Value="U" />
                                    <dxe:ListEditItem Text="Inclusão" Value="I" />
                                    <dxe:ListEditItem Text="Exclusão" Value="E" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="padding-left: 5px">
                            <dxe:ASPxTextBox ID="txtFiltroAlteracao" runat="server" 
                                Width="100%" ClientInstanceName="txtFiltroAlteracao">
                            </dxe:ASPxTextBox>
                        </td>
                        <td style="width: 80px; padding-left: 5px">
                            <dxe:ASPxButton ID="ASPxButton3" runat="server" 
                                Text="Buscar" Width="100%" onclick="ASPxButton3_Click">
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="2">
                            <dxe:ASPxMemo ID="MemoWhere" runat="server" Height="71px" Width="100%" 
                                ClientInstanceName="MemoWhere">
                            </dxe:ASPxMemo>
                        </td>
                        <td style="width: 80px; padding-left: 5px">
                            <dxe:ASPxButton ID="btnAnd" runat="server"  Text="E"
                                Width="100%">
                                <ClientSideEvents Click="function(s, e) {
    var qtdBotaoWhereJS  = MemoWhere.cp_qtdWhere;//sempre vai ser zero
     
    var qtdJaExistenteHFGeral = hfGeral.Get(&quot;cp_qtdWhere&quot;);   
    //se a quantidade armazenada no hfGeral for igual a ZERO
    if(qtdBotaoWhereJS == qtdJaExistenteHFGeral)
    {
         qtdBotaoWhereJS = 1;  
    }
    else if(qtdJaExistenteHFGeral &gt; 0)
    {
        qtdBotaoWhereJS = qtdJaExistenteHFGeral + 1;
    }

    hfGeral.Set(&quot;cp_qtdWhere&quot;, qtdBotaoWhereJS);
	e.processOnServer = f_MontaWhere('E', qtdBotaoWhereJS);
    
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80px; padding-left: 5px">
                            <dxe:ASPxButton ID="btnOr" runat="server"  Text="Ou"
                                Width="100%">
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
            <td>
                &nbsp;
                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                </dxhf:ASPxHiddenField>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td>
                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" Width="40px" Caption=" ">
                            <Settings AllowAutoFilter="False" />
                            <DataItemTemplate>
                                <%# getBotaoVisualizar()%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DATA_OPERACAO" VisibleIndex="1"
                            Width="110px">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesDateEdit>
                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Equals" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TABELA" VisibleIndex="2" Caption="Nome da Tabela"
                            Width="200px">
                            <Settings AutoFilterCondition="Contains" AllowAutoFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TipoOperacao" VisibleIndex="3" Caption="Operação Realizada"
                            Width="140px">
                            <Settings AutoFilterCondition="Contains" AllowAutoFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="USUARIO" VisibleIndex="4">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="200" AlwaysShowPager="True">
                    </SettingsPager>
                    <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" />
                </dxwgv:ASPxGridView>
            </td>
            <td class="style2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
