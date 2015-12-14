using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace TestStudio.Automation.TestManager.libCommon.Class
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public TValue this[TKey key]
        {
            get{
                if (!this.ContainsKey(key))
                {
                    return default(TValue);
                }
                else
                {
                    return base[key];
                }
            }
            set{
                base[key] = value;
            }
        }
        public SerializableDictionary() { }
        public void WriteXml(XmlWriter write)       // Serializer
        {
            try
            {
                XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
                XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

                write.WriteStartElement("SerializableDictionary");
                foreach (KeyValuePair<TKey, TValue> kv in this)
                {
                    write.WriteStartElement("key");
                    KeySerializer.Serialize(write, kv.Key);
                    write.WriteEndElement();
                    write.WriteStartElement("value");
                    ValueSerializer.Serialize(write, kv.Value);
                    write.WriteEndElement();
                }
                write.WriteEndElement();
            }
            catch(Exception)
            {

            }
        }

        public void WriteXml(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<TKey, TValue>));
                xmlFormatter.Serialize(fileStream, this);
                fileStream.Close();
            }
        }
        public void ReadXml(XmlReader reader)       // Deserializer
        {
            try
            {
                reader.Read();
                XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
                XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));
                reader.ReadStartElement("SerializableDictionary");
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.ReadStartElement("key");
                    TKey tk = (TKey)KeySerializer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadStartElement("value");
                    TValue vl = (TValue)ValueSerializer.Deserialize(reader);
                    reader.ReadEndElement();
                    this.Add(tk, vl);
                    reader.MoveToContent();
                }
                reader.ReadEndElement();
                reader.ReadEndElement();
            }
            catch (Exception)
            {
            }
        }

        public static SerializableDictionary<TKey,TValue> InitWithFile(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<TKey, TValue>));
                SerializableDictionary<TKey, TValue> t = (SerializableDictionary<TKey, TValue>)xmlFormatter.Deserialize(fileStream);
                fileStream.Close();
                return t;
            }
        }
        public void ReadXmlFile(string path)
        {
#if false
            XmlTextReader reader = new XmlTextReader(path);
            ReadXml(reader);
            reader.Close();
            return;           
#else
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<TKey, TValue>));
                SerializableDictionary<TKey, TValue> t = (SerializableDictionary<TKey, TValue>)xmlFormatter.Deserialize(fileStream);
                fileStream.Close();
                this.Clear();
                foreach (KeyValuePair<TKey,TValue> kv in t)
                {
                    this.Add(kv.Key, kv.Value);
                }
            }
#endif

        }
        public XmlSchema GetSchema()
        {
            return null;
        }
    }

    public class DictionaryEx:SerializableDictionary<string,object>
    {
        public DictionaryEx()
        {
        }
    }
}
