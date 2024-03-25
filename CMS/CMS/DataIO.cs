using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CMS
{
    // Class for serialization and deserialization
    public class DataIO
    {
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if(serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());

                using(MemoryStream ms = new MemoryStream())
                {
                    serializer.Serialize(ms, serializableObject);
                    ms.Position = 0;
                    xmlDocument.Load(ms);
                    xmlDocument.Save(fileName);
                    ms.Close();
                }
            }
            catch(Exception e)
            {

            }


        }

        public T DeSerializeObject<T>(string fileName)
        {
            if(string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using(StreamReader sr = new StreamReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);

                    using(XmlReader xmlr = new XmlTextReader(sr))
                    {
                        objectOut = (T)serializer.Deserialize(sr);
                        xmlr.Close();
                    }

                    sr.Close();

                }
            }
            catch(Exception e)
            {

            }

            return objectOut;
        }
    }
}
