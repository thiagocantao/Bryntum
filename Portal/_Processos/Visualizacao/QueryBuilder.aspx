<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryBuilder.aspx.cs" Inherits="_Processos_Visualizacao_QueryBuilder" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function OnSaveCommandExecute(s, e) {
            var t = s.GetJsonQueryModel();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxxr:ASPxQueryBuilder ID="queryBuilder" runat="server" ClientInstanceName="queryBuilder" OnSaveQuery="queryBuilder_SaveQuery">
                <ClientSideEvents SaveCommandExecute="OnSaveCommandExecute" EndCallback="function(s, e) {
	alert(s.cpTeste);
}" />
            </dxxr:ASPxQueryBuilder>
        </div>
    </form>
</body>
</html>
