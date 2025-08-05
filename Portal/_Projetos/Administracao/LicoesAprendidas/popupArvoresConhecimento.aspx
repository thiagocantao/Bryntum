<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupArvoresConhecimento.aspx.cs" Inherits="_Projetos_Administracao_LicoesAprendidas_popupArvoresConhecimento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .iniciaisMaiusculas {
            text-transform: capitalize !important
        }

        .dxtlFocusedNode_MaterialCompact {
            background: # none;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: flex; flex-direction: column">
            <div id="divTreeList" style="visibility: hidden; padding-top: 5px; padding-left: 10px; padding-right: 5px">
                <dxwtl:ASPxTreeListExporter ID="ASPxTreeListExporter2" runat="server" OnRenderBrick="ASPxTreeListExporter1_RenderBrick" TreeListID="tlArvore">
                </dxwtl:ASPxTreeListExporter>
                <dxwtl:ASPxTreeList ID="tlArvore" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="CodigoElementoArvore"
                    ParentFieldName="CodigoElementoArvoreSuperior" ClientInstanceName="tlArvore" Width="100%" OnDataBound="tlArvore_DataBound">
                    <Columns>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="CodigoElemento" FieldName="CodigoElementoArvore" Name="CodigoElemento" ShowInFilterControl="Default" Visible="False" VisibleIndex="1">
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="DescricaoElementoArvore" Name="TituloElemento" ShowInFilterControl="Default" VisibleIndex="2" Caption=" Descrição">
                            <DataCellTemplate>
                                <%# getDescricaoObjetosLista()%>
                            </DataCellTemplate>
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Criticidade do conhecimento" FieldName="DescricaoNIvelCriticidade" Name="DescricaoNIvelCriticidade" ShowInFilterControl="Default" VisibleIndex="3">
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="CodigoElementoSuperior" FieldName="CodigoElementoArvoreSuperior" Name="CodigoElementoSuperior" ShowInFilterControl="Default" Visible="False" VisibleIndex="4">
                        </dxwtle:TreeListTextColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" />
                    <SettingsBehavior AllowSort="False" AutoExpandAllNodes="True" ExpandNodesOnFiltering="True" AllowFocusedNode="True" />
                    <SettingsSelection Enabled="True" />
                    <SettingsPopup>
                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                    </SettingsPopup>
                    <ClientSideEvents SelectionChanged="function(s, e) {
     e.processOnServer = false;
      var noClicado = s.GetFocusedNodeKey();      
       var buttonElement = btnSelecionar.GetMainElement();
       if(s.IsNodeSelected(noClicado))
       {      
                popup_aco_CodigoArvoreConhecimento = noClicado;
                btnSelecionar.SetEnabled(true);                  
                buttonElement.title = '';  
                var listaSelecionados = s.GetVisibleSelectedNodeKeys();
                for(var i = 0; i &lt;= listaSelecionados.length; i++)
                {
                         if( listaSelecionados[i] !=  noClicado )
                         {
                                s.SelectNode( listaSelecionados[i] , false );
                                
                         }
                 }
         }
         else
         {
                btnSelecionar.SetEnabled(false);                  
                buttonElement.title = 'Para habilitar este botão selecione um item de conhecimento';  
         }

}"
                        Init="function(s, e) {	
                        var listaSelecionados = s.GetVisibleSelectedNodeKeys();
                         var buttonElement = btnSelecionar.GetMainElement();
                        if(listaSelecionados.length &gt; 0){
                             popup_aco_CodigoArvoreConhecimento = listaSelecionados[0];
                             btnSelecionar.SetEnabled(true);                  
                             buttonElement.title = '';  
                        }
                        else
                        {
                           
                            btnSelecionar.SetEnabled(false);                  
                            buttonElement.title = 'Para habilitar este botão selecione um item de conhecimento';  
                        }
                        
                         var sHeight = Math.max(0, document.documentElement.clientHeight) - 120;
                              s.SetHeight(sHeight);
                              document.getElementById('divTreeList').style.visibility = 'visible';
                              document.getElementById('divDosBotoes').style.visibility = 'visible';


}" />
                </dxwtl:ASPxTreeList>

            </div>
            <div>
                <div id="divDosBotoes" style="display: flex; flex-direction: row-reverse; align-content: flex-end;visibility:hidden">
                    <div style="margin: 5px">
                        <dxcp:ASPxButton ID="btnFechar" ClientInstanceName="btnFechar" runat="server" Text="Fechar" CssClass="iniciaisMaiusculas" Width="100px">
                            <ClientSideEvents Click="function(s, e) {
    //window.top.retornoModal = popup_aco_CodigoArvoreConhecimento.toString();
    window.top.fechaModal();
}" />
                        </dxcp:ASPxButton>
                    </div>
                    <div style="margin: 5px">
                        <dxcp:ASPxButton ID="btnSelecionar" ClientInstanceName="btnSelecionar"  ToolTip="Para habilitar este botão selecione um item de conhecimento" ClientEnabled="false" runat="server" Text="Selecionar" CssClass="iniciaisMaiusculas" Width="100px">
                            <ClientSideEvents Click="function(s, e) {
	    window.top.retornoModal = popup_aco_CodigoArvoreConhecimento.toString();
                    window.top.fechaModal();
}" />
                        </dxcp:ASPxButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var popup_aco_CodigoArvoreConhecimento;
    </script>
</body>
</html>
