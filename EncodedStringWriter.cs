// Decompiled with JetBrains decompiler
// Type: ManagedXml.EncodedStringWriter
// Assembly: ManagedXml, Version=1.2013.13122.632, Culture=neutral, PublicKeyToken=null
// MVID: 5AC6EB65-12DF-4B54-AE76-7A6C8A0390E4
// Assembly location: C:\Users\asear\source\repos\PokeSheet\PokeSheet\PtaSheet\Libs\ManagedXml.dll

using System;
using System.IO;
using System.Text;

namespace ManagedXml
{
  public class EncodedStringWriter : StringWriter
  {
    private Encoding _encoding;

    public EncodedStringWriter()
    {
    }

    public EncodedStringWriter(IFormatProvider formatProvider)
      : base(formatProvider)
    {
    }

    public EncodedStringWriter(StringBuilder sb)
      : base(sb)
    {
    }

    public EncodedStringWriter(StringBuilder sb, IFormatProvider formatProvider)
      : base(sb, formatProvider)
    {
    }

    public EncodedStringWriter(Encoding encoding) => this._encoding = encoding;

    public EncodedStringWriter(IFormatProvider formatProvider, Encoding encoding)
      : base(formatProvider)
      => this._encoding = encoding;

    public EncodedStringWriter(StringBuilder sb, Encoding encoding)
      : base(sb)
      => this._encoding = encoding;

    public EncodedStringWriter(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
      : base(sb, formatProvider)
      => this._encoding = encoding;

    public override Encoding Encoding => this._encoding ?? base.Encoding;
  }
}
