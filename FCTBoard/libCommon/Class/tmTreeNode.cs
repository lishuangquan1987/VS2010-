using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public class tmTreeNode
    {
        object m_data=null;
        List<tmTreeNode> m_ChildNodes = new List<tmTreeNode>();
        public tmTreeNode()
        {

        }
        public tmTreeNode(object data)
        {
            m_data = data;
        }

        public void SetRepresentObject(object obj)
        {
            m_data=obj;
        }

        public object RepresentObject()
        {
            return m_data;
        }
        
        public List<tmTreeNode> ChildNodes()
        {
            return m_ChildNodes;
        }

        public void AddChildNode(tmTreeNode node)
        {
            m_ChildNodes.Add(node);
        }
    }
}
