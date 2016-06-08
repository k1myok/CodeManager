using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Xml.Linq
{
    public static class XElementExtensions
    {
        public static XElement FindElement(this XElement element, string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var pathItems = path.Split('/');
            var item = pathItems[0];
            var childElement = element.FindElement(p => p.Name.LocalName == item);
            if (childElement == null)
                return null;

            if (pathItems.Length == 1)
                return childElement;
            else
                return childElement.FindElement(string.Join("/", pathItems.Skip(1).Take(pathItems.Length - 1)));
        }


        public static XElement FindElement(this XElement element, Func<XElement, bool> func)
        {
            foreach (var item in element.Elements())
            {
                if (func.Invoke(item))
                    return item;
                else
                {
                    var result = item.FindElement(func);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }

        public static string GetAttributeValue(this XElement element, string attributeName)
        {
            var attribute = element.Attribute(XName.Get(attributeName));
            if (attribute == null)
                return null;

            return attribute.Value;
        }
    }
}
