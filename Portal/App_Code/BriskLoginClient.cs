using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

/// <summary>
/// Clase utilizada para autenticar el servidor con los servicios de otro servidor
/// </summary>
public class BriskLoginClient
{
    public BriskLoginClient()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //'UrlServiceV1': 'http://200.175.180.60:40222/dv2/api/v1/',

    /**
     * curl -X POST --header 'Content-Type: application/json-patch+json' --header 'Accept: application/json' --header 'cod-entidade-contexto: 111' -d '{ \ 
       "email": "andre%40cdis.com.br", \ 
       "senha": "Rsenha22", \ 
       "rememberMe": true \ 
     } \ 
     ' 'http://cdis.inf.br:40222/dv2/api/v1/conta'
     */

    public string login(string user, string password)
    {
        posDataToLogin(user, password);
        return "";
        //WebClient wc = new WebClient();
        //wc.Headers["Accept"] = "application/json";
        //wc.Headers["Content-Type"] = "application/json-patch+json";
        //wc.Headers["cod-entidade-contexto"] = "111";
        //Credentials credentials = new Credentials(user, password); 
        //string url = "http://cdis.inf.br:40222/dv2/api/v1/conta";
        //string serializex = JsonConvert.SerializeObject(credentials);
        //System.Diagnostics.Debug.Write(serializex);
        //return wc.UploadString(url, serializex);
    }

    private void posDataToLogin(string user, string pswd)
    {
        Credentials credentials = new Credentials(user, pswd);
        string serializex = JsonConvert.SerializeObject(credentials);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.cdis.inf.br:40222/dv2/api/v1/conta");
        request.Headers["cod-entidade-contexto"] = "111";
        request.Method = "POST";

        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        Byte[] byteArray = encoding.GetBytes(serializex);

        request.ContentLength = byteArray.Length;
        request.ContentType = @"application/json";

        using (Stream dataStream = request.GetRequestStream())
        {
            dataStream.Write(byteArray, 0, byteArray.Length);
        }
        long length = 0;
        try
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                length = response.ContentLength;
            }
        }
        catch (WebException ex)
        {
            System.Diagnostics.Debug.Write(ex.Message);
            // Log exception and throw as for GET example above
        }
    }

    /*
     
var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
httpWebRequest.ContentType = "application/json";
httpWebRequest.Method = "POST";

using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
{
    string json = "{\"user\":\"test\"," +
                  "\"password\":\"bla\"}";

    streamWriter.Write(json);
    streamWriter.Flush();
    streamWriter.Close();
}

var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
{
    var result = streamReader.ReadToEnd();
}     
     
     */


    private class Credentials
    {
        private string Email;
        private string Password;
        //private bool RemenberMe;

        public Credentials(string user, string password)
        {
            Email = user;
            Password = password;
        }

        public string email
        {
            get
            {
                return Email;
            }

            set
            {
                Email = value;
            }
        }

        public string password
        {
            get
            {
                return Password;
            }

            set
            {
                Password = value;
            }
        }

        //public bool rememberMe
        //{
        //    get
        //    {
        //        return RemenberMe;
        //    }

        //    set
        //    {
        //        RemenberMe = value;
        //    }
        //}
    }
}