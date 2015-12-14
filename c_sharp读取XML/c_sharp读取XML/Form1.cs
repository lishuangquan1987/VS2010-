using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

namespace c_sharp读取XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool LoadTmFile(string filepath, Dictionary dic)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            try
            {
                XmlNodeList nodeList = doc.GetElementsByTagName("Module");
                if (0 == nodeList.Count) throw new Exception("no Module");
                XmlElement module = nodeList.Item(0) as XmlElement;
                string name = module.GetAttribute("name");
                if (name == null)
                {
                    name = "Unkown Module";
                }
                dic["name"] = name;

                XmlElement element;
                //ui
                nodeList = module.GetElementsByTagName("ui");
                if (0 == nodeList.Count) throw new Exception("no specical user interface path");
                element = nodeList.Item(0) as XmlElement;
                dic["ui"] = element.InnerText;

                //engine
                nodeList = module.GetElementsByTagName("engine");
                if (nodeList == null) throw new Exception("no specical test engine path");
                element = nodeList.Item(0) as XmlElement;
                string txt = element.InnerText;
                dic["engine"] = element.InnerText;

                //instruments
                nodeList = module.GetElementsByTagName("instruments");
                if (nodeList.Count != 0)
                {
                    element = nodeList.Item(0) as XmlElement;
                    nodeList = element.GetElementsByTagName("instrument");
                    List<string> arr = new List<string>();
                    foreach (XmlNode node in nodeList)
                    {
                        arr.Add(node.InnerText);
                    }
                    dic["instruments"] = arr.ToArray();
                }
            }
            catch (System.Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
