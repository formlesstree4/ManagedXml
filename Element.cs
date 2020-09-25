// Decompiled with JetBrains decompiler
// Type: ManagedXml.Element
// Assembly: ManagedXml, Version=1.2013.13122.632, Culture=neutral, PublicKeyToken=null
// MVID: 5AC6EB65-12DF-4B54-AE76-7A6C8A0390E4
// Assembly location: C:\Users\asear\source\repos\PokeSheet\PokeSheet\PtaSheet\Libs\ManagedXml.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ManagedXml
{
  public class Element
  {
    private Encoding _encoding;

    public string Name { get; set; }

    public string Value { get; set; }

    public Element Parent { get; set; }

    public List<Element> Children { get; set; }

    public List<Attribute> Attributes { get; set; }

    public Encoding Encoding
    {
      get => this._encoding;
      set
      {
        foreach (Element child in this.Children)
          child.Encoding = value;
        this._encoding = value;
      }
    }

    private Element()
    {
      this.Name = string.Empty;
      this.Value = string.Empty;
      this.Children = new List<Element>();
      this.Attributes = new List<Attribute>();
      this.Encoding = Encoding.Default;
    }

    private static void WriteParameters(ref XmlWriter xmlWriter, Element parms)
    {
      xmlWriter.WriteStartElement(parms.Name);
      if (parms.Attributes.Count > 0)
      {
        foreach (Attribute attribute in parms.Attributes)
          xmlWriter.WriteAttributeString(attribute.Name, attribute.Value);
      }
      if (parms.Children.Count > 0)
      {
        foreach (Element child in parms.Children)
          Element.WriteParameters(ref xmlWriter, child);
      }
      else if (parms.Value.StartsWith("<![CDATA["))
        xmlWriter.WriteRaw(parms.Value);
      else
        xmlWriter.WriteString(parms.Value);
      xmlWriter.WriteEndElement();
    }

    public override string ToString()
    {
      EncodedStringWriter encodedStringWriter = new EncodedStringWriter(this.Encoding);
      XmlWriter xmlWriter = XmlWriter.Create((TextWriter) encodedStringWriter, new XmlWriterSettings()
      {
        CheckCharacters = true,
        CloseOutput = true,
        Indent = true
      });
      xmlWriter.WriteStartDocument();
      Element.WriteParameters(ref xmlWriter, this);
      xmlWriter.WriteEndDocument();
      xmlWriter.Flush();
      xmlWriter.Close();
      return encodedStringWriter.ToString();
    }

    public static Element Create() => Element.Create(string.Empty);

    public static Element Create(string name) => Element.Create(name, string.Empty);

    public static Element Create(string name, string value) => Element.Create(name, value, new List<Attribute>());

    public static Element Create(string name, string value, List<Attribute> attributes) => Element.Create(name, value, attributes, Encoding.Default);

    public static Element Create(
      string name,
      string value,
      List<Attribute> attributes,
      Encoding encoding)
    {
      return new Element()
      {
        Name = name,
        Value = value,
        Attributes = attributes,
        Encoding = encoding,
        Children = new List<Element>()
      };
    }

    public static Element Create(string name, List<Element> children) => Element.Create(name, children, Encoding.Default);

    public static Element Create(string name, List<Element> children, Encoding encoding) => new Element()
    {
      Name = name,
      Value = string.Empty,
      Encoding = encoding,
      Children = children,
      Attributes = new List<Attribute>()
    };

    public Element this[string name]
    {
      get => this.GetElement(name);
      set
      {
        if (this.Children.Any<Element>((Func<Element, bool>) (f => f.Name == name)))
        {
          int index = this.Children.IndexOf(this.Children.First<Element>((Func<Element, bool>) (f => f.Name == name)));
          this.Children.RemoveAt(index);
          this.Children.Insert(index, value);
        }
        else
          this.Children.Add(value);
      }
    }

    public string this[string attrName, string ifNull]
    {
      get => this.GetAttributeValue(attrName);
      set
      {
        if (this.Attributes.Any<Attribute>((Func<Attribute, bool>) (f => f.Name == attrName)))
        {
          int index = this.Attributes.IndexOf(this.Attributes.First<Attribute>((Func<Attribute, bool>) (f => f.Name == attrName)));
          this.Attributes.RemoveAt(index);
          this.Attributes.Insert(index, Attribute.Create(attrName, string.IsNullOrEmpty(value) ? ifNull : value));
        }
        else
          this.Attributes.Add(Attribute.Create(attrName, string.IsNullOrEmpty(value) ? ifNull : value));
      }
    }

    public string GetAttributeValue(string attribute) => this.GetAttributeValue(attribute, string.Empty);

    public string GetAttributeValue(string attribute, string ifNotExists) => this.Attributes.Exists((Predicate<Attribute>) (a => a.Name == attribute)) ? this.Attributes.First<Attribute>((Func<Attribute, bool>) (a => a.Name == attribute)).Value : ifNotExists;

    public Element GetElement(string name) => this.Children.Exists((Predicate<Element>) (e => e.Name == name)) ? this.Children.First<Element>((Func<Element, bool>) (e => e.Name == name)) : (Element) null;
  }
}
