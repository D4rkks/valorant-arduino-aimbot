// Decompiled with JetBrains decompiler
// Type: unknown.Properties.Settings
// Assembly: unknown, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 835C0890-0818-42AB-84B4-3BFA3176FC6A
// Assembly location: C:\Users\jao\Downloads\GamerVoid\Dumps\unknown.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace unknown.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}
