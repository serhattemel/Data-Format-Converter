using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Data_Format_Converter
{
    internal class Conversion
    {
        XmlDocument doc = new XmlDocument();
        Data data = new Data();
        public string ConvertXMLToJSON(string xml)
        {
            try
            {
                doc.LoadXml(xml);
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                var jsonObject = JsonConvert.DeserializeObject(jsonString);

                // Serialize the object back to a nicely formatted JSON string
                string beautifiedJson = JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);

                return beautifiedJson;
            }
            catch
            {
                return "Hatalı XML verisi girdiniz.";
            }
        }
        public string ConvertJSONToXML(string json)
        {
            try
            { 
            // JSON verisini XML'e dönüştürme
            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(json, "Root");

            // XML'i string'e dönüştürme
            string xmlString;
            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlDoc.WriteTo(xmlWriter);
                }
                xmlString = stringWriter.ToString();
            }

            return xmlString;
            }
            catch
            {
                return "Hatalı JSON verisi girdiniz.";
            }
        }

        public string ConvertXMLToXSD(string xml)
        {
            try
            {
                // XML stringini yükleyin
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                // XmlSchemaInference kullanarak XSD şemasını türetin
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                XmlSchemaInference schemaInference = new XmlSchemaInference();

                schemaSet = schemaInference.InferSchema(new XmlTextReader(new StringReader(xml)));
                string x = "";
                foreach (XmlSchema s in schemaSet.Schemas())
                {
                    s.Write(Console.Out);
                    x = ConvertXmlSchemaToString(s);
                }

                return x;
            }
            catch
            {
                return "Hatalı XML verisi girdiniz.";
            }

        }
        static string ConvertXmlSchemaToString(XmlSchema xmlSchema)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = System.Xml.Formatting.Indented;

                    // Write the XmlSchema to the XmlTextWriter
                    xmlSchema.Write(xmlTextWriter);

                    // Flush the writer to make sure everything is written
                    xmlTextWriter.Flush();

                    // Get the string representation of the XmlSchema
                    return stringWriter.ToString();
                }
            }
        }

        public string ConvertJSONToXSD(string json)
        {
            try
            {
                string xsd;
                xsd = ConvertJSONToXML(json);
                xsd = ConvertXMLToXSD(xsd);
                return xsd;
            }
            catch
            {
                return "Hatalı JSON verisi girdiniz.";
            }
        }

        public string ConvertXSDToXML(string xsd)
        {
            // XSD verisini XML'e dönüştürme
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xsd);
            string xmlString = "";
            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlDoc.WriteTo(xmlWriter);
                }
                xmlString = stringWriter.ToString();
            }
            return xmlString;
        }

        public string ConvertXSDToJSON(string xsd)
        {
            // XSD verisini XML'e dönüştürme
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xsd);
            string xmlString = "";
            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlDoc.WriteTo(xmlWriter);
                }
                xmlString = stringWriter.ToString();
            }

            // XML'i JSON'a dönüştürme
            XmlDocument jsonDoc = new XmlDocument();
            jsonDoc.LoadXml(xmlString);
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeXmlNode(jsonDoc);
            var jsonObject = JsonConvert.DeserializeObject(jsonString);

            // Serialize the object back to a nicely formatted JSON string
            string beautifiedJson = JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);

            return beautifiedJson;
        }
        public class Utf8StringWriter : System.IO.StringWriter
        {
            public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
        }
    }
}
