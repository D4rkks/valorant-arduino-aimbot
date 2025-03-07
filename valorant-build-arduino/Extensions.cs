// Decompiled with JetBrains decompiler
// Type: unknown.Extensions
// Assembly: unknown, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 835C0890-0818-42AB-84B4-3BFA3176FC6A
// Assembly location: C:\Users\jao\Downloads\GamerVoid\Dumps\unknown.exe

namespace unknown
{
  public static class Extensions
  {
    public static bool IsNumeric(this string s) => float.TryParse(s, out float _);
  }
}
