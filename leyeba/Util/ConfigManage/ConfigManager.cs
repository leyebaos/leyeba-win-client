using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace Util.ConfigManage
{
    public class ConfigManager
    {
        private XDocument xdoc;

        public XDocument XDoc
        {
            get { return xdoc; }
            set { xdoc = value; }
        }
        
        private string xmlName = string.Empty;
        public ConfigManager(string filepath)
        {
            xmlName = filepath;
            if (!File.Exists(xmlName))
            {
                xdoc = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\"?><document></document>");
                xdoc.Save(xmlName);
            }
            xdoc = XDocument.Load(xmlName);
        }

        public bool IsExitElement(string xpath)
        {
            if (GetXElement(xpath) != null) return true;
            return false;
        }
        /// <summary>
        /// 根据路径获取一个元素集合
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XElement GetXElement(string xpath)
        {
            if (xdoc == null) return null;
            return xdoc.Root.XPathSelectElements(xpath).FirstOrDefault();
        }

        public void AppendXElement(string text)
        {
            XElement ele = XElement.Parse(text);
            xdoc.Root.Add(ele);
            xdoc.Save(xmlName);
            xdoc = XDocument.Load(xmlName); 
        }
        /// <summary>
        /// 更新配置节点的值
        /// </summary>
        /// <param name="noteName">节点的name的值</param>
        /// <param name="noteAttri">节点的属性名</param>
        /// <param name="value">节点的属性新值</param>
        /// <returns></returns>
        public bool UpdateNode(string noteName, string parentPath, string value)
        {
            var node = GetNode<XElement>(xdoc.Root, parentPath + "/" + noteName);
            if (node == null)
            {
                XElement parentNode = GetNode<XElement>(xdoc.Root, parentPath);
                if (parentNode == null)
                    return false;
                node = XElement.Parse("<"+noteName+"></"+noteName+">");
                parentNode.Add(node);
            }
            node.SetValue(value);
            return true;
        }
        /// <summary>
        /// 更新指定userid下的子节点的值
        /// </summary>
        /// <param name="noteName"></param>
        /// <param name="value"></param>
        /// <param name="useid"></param>
        /// <returns></returns>
        public bool UpdateNodeAttributes(string xpath, string value)
        {
            var node = GetNode<XAttribute>(xdoc.Root, xpath);
            if (node != null)
            {                
                node.SetValue(value);
                return true;
            }
            return false;
        }

        public void UpdateAllAttribute(string xpath, string noteName, string value)
        {
            List<XElement> elelst = xdoc.Root.XPathSelectElements(xpath).ToList();
            if (elelst == null || elelst.Count == 0) return;
            foreach (XElement ele in elelst)
            {
                ele.SetAttributeValue(noteName, value);
            }
        }

        public void Save()
        {
            xdoc.Save(xmlName);
        }

        /// <summary>
        /// 获取给定xpath对应的第一个节点的值，不存在节点返回null
        /// </summary>
        /// <param name="parentElement">父节点</param>
        /// <param name="xPath">搜索路径</param>
        /// <returns>第一个匹配节点值</returns>
        public string GetNodeValue(XElement parentElement, string xPath,bool isAttribute=false)
        {
            if (isAttribute)
            {
                var node = GetNode<XAttribute>(parentElement, xPath);
                if (node != null)
                    return node.Value;
            }
            else
            {
                var node = GetNode<XElement>(parentElement, xPath);
                if (node != null)
                    return node.Value;
            }
            return null;
        }

        /// <summary>
        /// 获取指定类型的节点
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="parentElement">父节点</param>
        /// <param name="xPath">搜索xpath</param>
        /// <returns>parentElement下第一个匹配的节点不存在返回T默认值</returns>
        public T GetNode<T>(XElement parentElement, string xPath)
        {
            if (parentElement == null)
                return default(T);

            var node = ((IEnumerable)parentElement.XPathEvaluate(xPath)).Cast<T>().FirstOrDefault();
            if (node != null)
                return node;
            return default(T);
        }

        /// <summary>
        /// 从当前文档根节点搜索给定xpath对应的第一个节点的值，不存在节点返回空
        /// </summary>
        /// <param name="xPath">搜索路径</param>
        /// <returns>第一个匹配节点值</returns>
        public string GetNodeValue(string xPath, bool isAttribute = false)
        {
            if (xdoc == null)
                return null;
            return GetNodeValue(xdoc.Root, xPath, isAttribute);
        }

        public List<XElement> GetElements(string expression)
        {
            if (xdoc == null)
                return null;
            return xdoc.Root.XPathSelectElements(expression).ToList();
        }
    }
}
