using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Zyck.Frame.Extensions
{
    public class XmlConverter
    {
        public static string Serialize<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            serializer.Serialize(writer, obj);
            string xml = writer.ToString();
            writer.Close();
            writer.Dispose();

            return xml;
        }

        public static T Deserialize<T>(string xml, string defaultNamespace = "xml")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(defaultNamespace));
            StringReader reader = new StringReader(xml);
            T result = (T)(serializer.Deserialize(reader));
            reader.Close();
            reader.Dispose();

            return result;
        }

    }

}
