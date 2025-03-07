// Decompiled with JetBrains decompiler
// Type: unknown.IntUtil
// Assembly: unknown, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 835C0890-0818-42AB-84B4-3BFA3176FC6A
// Assembly location: C:\Users\jao\Downloads\GamerVoid\Dumps\unknown.exe

namespace unknown
{
  public static class IntUtil
  {
    private static System.Random random;

    private static void Init()
    {
      if (IntUtil.random != null)
        return;
      IntUtil.random = new System.Random();
    }

    public static int Random(int min, int max)
    {
      IntUtil.Init();
      return IntUtil.random.Next(min, max);
    }
  }
}
