<%@ Page Language="C#" %>

<%

    String base64Image;
    base64Image = Request.Form["file"].ToString();
    Response.ContentType = "image/PNG";
    if (Request.Form["name"] != null) {
      Response.AppendHeader("content-disposition","attachment; filename=\""+Request.Form["name"]+"\"");
    }
    Response.BinaryWrite(System.Convert.FromBase64String(base64Image));

%>