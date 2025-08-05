<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="aparenciaSistema.aspx.cs" Inherits="administracao_aparenciaSistema" Title="Portal da Estratégia" %>
   <%@ MasterType VirtualPath="~/novaCdis.master" %>
  
  <asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
      <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
              <td>
              </td>
          </tr>
          <tr>
              <td style="height: 47px">
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
                                      <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                                          <dxe:aspxlabel id="lblTituloTela" runat="server" font-bold="True"
                                              text="Aparência do Sistema">
                              </dxe:aspxlabel>
                                      </td>
                                  </tr>
                              </table>
                          </td>
                      </tr>
                  </table>
              </td>
          </tr>
      </table>
      <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
              <td>
              </td>
              <td>
                              <dxe:aspxlabel id="lblSelecione" runat="server" text="Selecione a aparência:" ClientInstanceName="lblSelecione" >
                            </dxe:aspxlabel>
              </td>
              <td>
              </td>
          </tr>
          <tr>
              <td>
              </td>
              <td>
                              <dxe:aspxcombobox id="cmbModeloVisual" runat="server" valuetype="System.String" clientinstancename="cmbModeloVisual"
                                  autopostback="True" onselectedindexchanged="cmbModeloVisual_SelectedIndexChanged" Width="203px">
                                <Items>
                                    <dxe:ListEditItem Text="Padr&#227;o" Value="Default" />
                                    <dxe:ListEditItem Text="Aqua" Value="Aqua" />
                                    <dxe:ListEditItem Text="Black Glass" Value="BlackGlass" />
                                    <dxe:ListEditItem Text="Glass" Value="Glass" />
                                    <dxe:ListEditItem Text="Office2003 Blue" Value="Office2003Blue" />
                                    <dxe:ListEditItem Text="Office2003 Olive" Value="Office2003Olive" />
                                    <dxe:ListEditItem Text="Office2003 Silver" Value="Office2003Silver" />
                                    <dxe:ListEditItem Text="Plastic Blue" Value="PlasticBlue" />
                                    <dxe:ListEditItem Text="Red Wine" Value="RedWine" />
                                    <dxe:ListEditItem Text="Soft Orange" Value="SoftOrange" />
                                    <dxe:ListEditItem Text="Youthful" Value="Youthful" />
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) 
{
	//pnCallBackModelo.PerformCallback(cmbModeloVisual.GetValue());
	btnSalvar.SetEnabled(true);
}" />
                            </dxe:aspxcombobox>
              </td>
              <td>
              </td>
          </tr>
          <tr>
              <td style="height: 10px">
              </td>
              <td>
              </td>
              <td>
              </td>
          </tr>
          <tr>
              <td>
              </td>
              <td>
                              <dxwgv:aspxgridview id="gvModelo" runat="server" autogeneratecolumns="False" datasourceid="dsModelo"
                                  width="100%" keyfieldname="NomeUnidadeNegocio" ClientInstanceName="gvModelo" >
<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
<Columns>
<dxwgv:GridViewDataTextColumn FieldName="NomeUnidadeNegocio" Caption="Nome da unidade" VisibleIndex="0"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeNegocio" Caption="Sigla" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Caption="Gerente" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="EMail" Caption="E-mail" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="TelefoneContato1" Caption="Telefone" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
</Columns>

<Settings VerticalScrollBarMode="Visible"></Settings>
</dxwgv:aspxgridview>
              </td>
              <td>
              </td>
          </tr>
          <tr>
              <td style="height: 10px">
              </td>
              <td>
              </td>
              <td>
              </td>
          </tr>
          <tr>
              <td>
              </td>
              <td>
                  <dxe:aspxbutton id="btnSalvar" runat="server" onclick="btnSalvar_Click" text="Salvar a apar&#234;ncia escolhida" ClientInstanceName="btnSalvar" >
                      <ClientSideEvents Click="function(s, e) 
{
	if(cmbModeloVisual.GetSelectedIndex() == -1)
	{
		window.top.mostraMensagem(&quot;A apar&#234;ncia do sistema deve ser selecionada.&quot;, 'Atencao', true, false, null);
		e.processOnServer = false;
	}	
}" />
                </dxe:aspxbutton>
                  <asp:SqlDataSource ID="dsModelo" runat="server" ConnectionString="<%$ ConnectionStrings:PortfolioConnectionString %>"
                      ProviderName="<%$ ConnectionStrings:PortfolioConnectionString.ProviderName %>"
                      SelectCommand="SELECT UnidadeNegocio.NomeUnidadeNegocio, UnidadeNegocio.SiglaUnidadeNegocio, Usuario.NomeUsuario, Usuario.EMail, Usuario.TelefoneContato1 FROM UnidadeNegocio INNER JOIN Usuario ON UnidadeNegocio.CodigoSuperUsuario = Usuario.CodigoUsuario WHERE (UnidadeNegocio.CodigoEntidade = @CodigoEntidade) &#13;&#10;ORDER BY UnidadeNegocio.NomeUnidadeNegocio">
                      <SelectParameters>
                          <asp:Parameter Name="CodigoEntidade" />
                      </SelectParameters>
                  </asp:SqlDataSource>
              </td>
              <td>
              </td>
          </tr>
      </table>
  </asp:Content>
