// Decompiled with JetBrains decompiler
// Type: ManagedXml.Attribute
// Assembly: ManagedXml, Version=1.2013.13122.632, Culture=neutral, PublicKeyToken=null
// MVID: 5AC6EB65-12DF-4B54-AE76-7A6C8A0390E4
// Assembly location: C:\Users\asear\source\repos\PokeSheet\PokeSheet\PtaSheet\Libs\ManagedXml.dll

namespace ManagedXml
{
  public class Attribute
  {
    public string Name { get; set; }

    public string Value { get; set; }

    public static Attribute Create(string name, string value) => new Attribute()
    {
      Name = name,
      Value = value
    };

    private Attribute()
    {
    }
  }
}
