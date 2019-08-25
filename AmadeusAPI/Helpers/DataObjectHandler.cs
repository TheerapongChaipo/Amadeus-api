using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AmadeusAPI.Helpers
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class DataObjectHandler
    {
        private static Type currentClass = typeof(DataObjectHandler);

        //deserialize string to object
        public static T DeSerializeXmlStringToObject<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlReader reader = XmlReader.Create(stringReader);
                XmlSerializer ser = new XmlSerializer(typeof(T));

                return (T)ser.Deserialize(reader);
            }
        }

        public static T DeSerializeJsonStringToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        //serialize object to string
        public static String SerializeObjToXmlString<T>(T obj)
        {
            MemoryStream memoryStream = new MemoryStream();

            // Create an XmlSerializer object to perform the serialization
            XmlSerializer xs = new XmlSerializer(obj.GetType());

            // Use the XmlSerializer object to serialize the data to the file
            xs.Serialize(memoryStream, obj);

            return UTF8Handler.UTF8ByteArrayToString(memoryStream.ToArray());
        }

        public static String SerializeObjToJsonString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static Stream SerializeObjToXmlStream<T>(T obj)
        {
            MemoryStream memoryStream = new MemoryStream();

            // Create an XmlSerializer object to perform the serialization
            XmlSerializer xs = new XmlSerializer(obj.GetType());

            // Use the XmlSerializer object to serialize the data to the file
            xs.Serialize(memoryStream, obj);

            memoryStream.Position = 0;
            return memoryStream;
        }

        public static Stream SerializeObjToJsonStream<T>(T obj)
        {
            return SerializeObjToJsonStream<T>(obj, false);
        }

        public static Stream SerializeObjToJsonStream<T>(T obj, bool ignoreNull)
        {
            string json;
            if (ignoreNull)
            {
                json = JsonConvert.SerializeObject(obj,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            else
            {
                json = JsonConvert.SerializeObject(obj);
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            MemoryStream memoryStream = new MemoryStream(byteArray);

            // Create an XmlSerializer object to perform the serialization           
            return memoryStream;
        }
    }
}