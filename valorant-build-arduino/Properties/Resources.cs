// Decompiled with JetBrains decompiler
// Type: unknown.Properties.Resources
// Assembly: unknown, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 835C0890-0818-42AB-84B4-3BFA3176FC6A
// Assembly location: C:\Users\jao\Downloads\GamerVoid\Dumps\unknown.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace unknown.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (unknown.Properties.Resources.resourceMan == null)
          unknown.Properties.Resources.resourceMan = new ResourceManager("unknown.Properties.Resources", typeof (unknown.Properties.Resources).Assembly);
        return unknown.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => unknown.Properties.Resources.resourceCulture;
      set => unknown.Properties.Resources.resourceCulture = value;
    }
  }
}
