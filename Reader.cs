// Decompiled with JetBrains decompiler
// Type: ManagedXml.Reader
// Assembly: ManagedXml, Version=1.2013.13122.632, Culture=neutral, PublicKeyToken=null
// MVID: 5AC6EB65-12DF-4B54-AE76-7A6C8A0390E4
// Assembly location: C:\Users\asear\source\repos\PokeSheet\PokeSheet\PtaSheet\Libs\ManagedXml.dll

using System.IO;
using System.Text;
using System.Xml;

namespace ManagedXml
{
  public class Reader
  {
    public Element Xml { get; private set; }

    public Reader(string XML)
    {
      Encoding encoding = Encoding.UTF8;
      if (XML.IndexOf("?>") > -1 && XML.IndexOf("encoding=") > -1 && XML.IndexOf("encoding=") < XML.IndexOf("?>"))
      {
        string str = XML.Substring(0, XML.IndexOf("?>") + 1);
        encoding = Encoding.GetEncoding(str.Remove(0, str.IndexOf("encoding")).TrimEnd('?', '>').Split('=')[1].Trim('"'));
      }
      XmlReader xmlReader = XmlReader.Create((Stream) new MemoryStream(encoding.GetBytes(XML)), new XmlReaderSettings()
      {
        IgnoreComments = true,
        IgnoreWhitespace = true
      });
      this.Xml = this.fGetElement(ref xmlReader);
      this.Xml.Encoding = encoding;
    }

    private Element fGetElement(ref XmlReader xmlReader)
    {
      Element element = Element.Create();
      while (xmlReader.Read())
      {
        if (xmlReader.NodeType == XmlNodeType.Element)
        {
          string name = xmlReader.Name;
          if (element.Name == "")
          {
            element.Name = name;
            if (xmlReader.HasAttributes)
            {
              xmlReader.MoveToFirstAttribute();
              do
              {
                element.Attributes.Add(Attribute.Create(xmlReader.Name, xmlReader.Value));
              }
              while (xmlReader.MoveToNextAttribute());
            }
          }
          else
            element.Children.Add(this.fGetElementContent(ref xmlReader));
        }
      }
      return element;
    }

    private Element fGetElementContent(ref XmlReader xmlReader)
    {
      Element element = Element.Create(xmlReader.Name);
      bool flag = !xmlReader.IsEmptyElement;
      if (xmlReader.HasAttributes)
      {
        xmlReader.MoveToFirstAttribute();
        do
        {
          element.Attributes.Add(Attribute.Create(xmlReader.Name, xmlReader.Value));
        }
        while (xmlReader.MoveToNextAttribute());
      }
      while (flag)
      {
        xmlReader.Read();
        switch (xmlReader.NodeType)
        {
          case XmlNodeType.None:
          case XmlNodeType.EndElement:
            flag = false;
            continue;
          case XmlNodeType.Element:
            element.Children.Add(this.fGetElementContent(ref xmlReader));
            continue;
          case XmlNodeType.Text:
            element.Value = xmlReader.Value;
            continue;
          case XmlNodeType.CDATA:
            element.Value = string.Format("<![CDATA[{0}]]>", (object) xmlReader.Value);
            continue;
          default:
            continue;
        }
      }
      return element;
    }
  }
}
