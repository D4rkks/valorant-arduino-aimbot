// Decompiled with JetBrains decompiler
// Type: unknown.IniFile
// Assembly: unknown, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 835C0890-0818-42AB-84B4-3BFA3176FC6A
// Assembly location: C:\Users\jao\Downloads\GamerVoid\Dumps\unknown.exe

using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace unknown
{
  public class IniFile
  {
    private string Path;
    private string EXE = "Config";

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(
      string Section,
      string Key,
      string Value,
      string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(
      string Section,
      string Key,
      string Default,
      StringBuilder RetVal,
      int Size,
      string FilePath);

    public IniFile(string IniPath = null) => this.Path = new FileInfo(IniPath ?? this.EXE + ".ini").FullName;

    public string Read(string Key, string Section = null)
    {
      StringBuilder RetVal = new StringBuilder((int) byte.MaxValue);
      IniFile.GetPrivateProfileString(Section ?? this.EXE, Key, "", RetVal, (int) byte.MaxValue, this.Path);
      return RetVal.ToString();
    }

    public void Write(string Key, string Value, string Section = null) => IniFile.WritePrivateProfileString(Section ?? this.EXE, Key, Value, this.Path);

    public void DeleteKey(string Key, string Section = null) => this.Write(Key, (string) null, Section ?? this.EXE);

    public void DeleteSection(string Section = null) => this.Write((string) null, (string) null, Section ?? this.EXE);

    public bool KeyExists(string Key, string Section = null) => this.Read(Key, Section).Length > 0;
  }
}
