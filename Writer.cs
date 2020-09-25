// Decompiled with JetBrains decompiler
// Type: ManagedXml.Writer
// Assembly: ManagedXml, Version=1.2013.13122.632, Culture=neutral, PublicKeyToken=null
// MVID: 5AC6EB65-12DF-4B54-AE76-7A6C8A0390E4
// Assembly location: C:\Users\asear\source\repos\PokeSheet\PokeSheet\PtaSheet\Libs\ManagedXml.dll

using System.Text;

namespace ManagedXml
{
  public class Writer
  {
    private Element _root;

    public Encoding Encoding
    {
      get => this._root.Encoding;
      set => this._root.Encoding = value;
    }

    public Element Root => this._root;

    public Writer()
      : this(string.Empty)
    {
    }

    public Writer(string name) => this._root = Element.Create(name);

    public Writer(Element e) => this._root = e;

    public Element AddElement(string name)
    {
      Element e = Element.Create(name);
      this.AddElement(e);
      return e;
    }

    public void AddElement(Element e)
    {
      e.Encoding = this._root.Encoding;
      this._root.Children.Add(e);
    }

    public Element AddElement(string name, Element e)
    {
      Element newElement = Element.Create(name);
      this.AddElement(newElement, e);
      return newElement;
    }

    public void AddElement(Element newElement, Element parent)
    {
      newElement.Encoding = parent.Encoding;
      newElement.Parent = parent;
      parent.Children.Add(newElement);
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(string.Format("Root Element: {0}", (object) this.Root.Name));
      stringBuilder.AppendLine(string.Format("Child Elements: {0}", (object) this.Root.Children.Count));
      return stringBuilder.ToString();
    }

    public string ToXML() => this._root.ToString();
  }
}
