<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/novaCdis.master"
    AutoEventWireup="true" CodeFile="workflow.aspx.cs" Inherits="workflow_workflow"
    Title="Portal da Estratégia" %>
 
<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <div width="<%=larguraObject %>" height="<%=alturaObject %>" >
        
        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" width="<%=larguraObject %>" height="<%=alturaObject %>" id="fluxo" align="middle">
				<param name="movie" value="fluxo.swf?caminhoWs=<%=webServicePath %>&codWorkflow=<%=codigoWorkflow %>&tipoFluxo=<%=tipoFluxo %>&width=<%=larguraFlashEdicao %>&height=<%=alturaFlashEdicao %>&codUsuario=<%=codigoUsuario %>&codFluxo=<%=codigoFluxo %>&xmlVersao=<%=xmlVersao %>" />
				<param name="quality" value="high" />
				<param name="bgcolor" value="#ffffff" />
				<param name="play" value="true" />
				<param name="loop" value="false" />
				<param name="wmode" value="direct" />
				<param name="scale" value="showall" />
				<param name="menu" value="false" />
				<param name="devicefont" value="true" />
				<param name="salign" value="" />
				<param name="allowScriptAccess" value="always" />
				<param name="allowFullScreen" value="true" />
    <!--[if !IE]>-->
				<object type="application/x-shockwave-flash" data="fluxo.swf?caminhoWs=<%=webServicePath %>&codWorkflow=<%=codigoWorkflow %>&tipoFluxo=<%=tipoFluxo %>&width=<%=larguraFlashEdicao %>&height=<%=alturaFlashEdicao %>&codUsuario=<%=codigoUsuario %>&codFluxo=<%=codigoFluxo %>&xmlVersao=<%=xmlVersao %>" width="<%=larguraObject %>" height="<%=alturaObject %>">
					<param name="movie" value="fluxo.swf?caminhoWs=<%=webServicePath %>&codWorkflow=<%=codigoWorkflow %>&tipoFluxo=<%=tipoFluxo %>&width=<%=larguraFlashEdicao %>&height=<%=alturaFlashEdicao %>&codUsuario=<%=codigoUsuario %>&codFluxo=<%=codigoFluxo %>&xmlVersao=<%=xmlVersao %>" />
					<param name="quality" value="high" />
					<param name="bgcolor" value="#ffffff" />
					<param name="play" value="true" />
					<param name="loop" value="false" />
					<param name="wmode" value="transparent" />
					<param name="scale" value="showall" />
					<param name="menu" value="false" />
					<param name="devicefont" value="true" />
					<param name="salign" value="" />
					<param name="allowScriptAccess" value="sameDomain" />
					<param name="allowFullScreen" value="true" />
				<!--<![endif]-->
					<a href="http://www.adobe.com/go/getflash">
						<img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Obter Adobe Flash Player" />
					</a>
				<!--[if !IE]>-->
				</object>
				<!--<![endif]-->
			</object>
         
    </div>
    

    </asp:Content>
