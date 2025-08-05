using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Descrição resumida de ConverterFormatUtil
/// </summary>
public static class ConverterFormatUtil
{
    public static string convertXMLToJSON(string xml)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        string jsonText = JsonConvert.SerializeXmlNode(doc);
        return jsonText != null ? jsonText.Replace("@","") : "";
    }

    public static string convertJSONToXML(string xml)
    {
        XmlDocument doc = JsonConvert.DeserializeXmlNode(xml);
        return doc != null ? doc.ToString() : "";
    }


}