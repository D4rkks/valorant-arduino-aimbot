// Decompiled with JetBrains decompiler
// Type: unknown.Program
// Assembly: unknown, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 835C0890-0818-42AB-84B4-3BFA3176FC6A
// Assembly location: C:\Users\jao\Downloads\GamerVoid\Dumps\unknown.exe

using System;
using System.Windows.Forms;

namespace unknown
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        MainScreen mainScreen = new MainScreen();
        mainScreen.Enabled = true;
        mainScreen.ShowDialog();
    }
  }
}
