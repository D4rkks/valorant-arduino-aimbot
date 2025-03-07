// unknown.PixelSearcher
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using unknown;

public class PixelSearcher
{
	private const int size = 60;

	private static float _rcsCurrent = 0f;

	private bool _holdingAimKey = false;

	private int _rcsHoldingTime = 0;

	public static int monitor = 0;

	private static bool holdingTriggerKey = false;

	private static float currentBHAOffset = 0f;

	private int followTimer = 0;

	private static DateTime lastShot = DateTime.Now;

	private static int[] RedDotKey = new int[9] { 1, 2, 4, 5, 6, 16, 18, 17, 20 };

	[DllImport("user32.dll")]
	private static extern short GetAsyncKeyState(int vKey);

	public PixelSearcher()
	{
		new Thread((ThreadStart)delegate
		{
			while (true)
			{
				if (Config.AppleStatus)
				{
					int num3 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.TRIGGER_HOTKEY, Config.currentMode));
					if (GetAsyncKeyState(RedDotKey[num3]) != 0)
					{
						if (!holdingTriggerKey)
						{
							holdingTriggerKey = true;
						}
					}
					else if (holdingTriggerKey)
					{
						holdingTriggerKey = false;
						followTimer = 0;
					}
					if (Config.AppleStatus)
					{
						if (Config.TriggerAimbot && GetAsyncKeyState(RedDotKey[num3]) != 0)
						{
							FlickBot();
						}
						if (GetAsyncKeyState(RedDotKey[num3]) != 0)
						{
							TriggerBot();
						}
					}
				}
				Thread.Sleep(1);
			}
		}).Start();
		new Thread((ThreadStart)delegate
		{
			while (true)
			{
				if (Config.SHITRECStatus)
				{
					NewRecoilSystem();
				}
				Thread.Sleep(1);
			}
		}).Start();
		new Thread((ThreadStart)delegate
		{
			while (true)
			{
				int screenWidth2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_X, Config.currentMode));
				int screenHeight2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_Y, Config.currentMode));
				int num2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.TRIGGER_HOTKEY, Config.currentMode));
				if (Config.BlueBerryStatus && GetAsyncKeyState(RedDotKey[num2]) == 0)
				{
					AimAssist(screenWidth2, screenHeight2);
				}
				Thread.Sleep(1);
			}
		}).Start();
		new Thread((ThreadStart)delegate
		{
			while (true)
			{
				int screenWidth = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_X, Config.currentMode));
				int screenHeight = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_Y, Config.currentMode));
				int num = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.AIMBOT_HOTKEY, Config.currentMode));
				if (GetAsyncKeyState(RedDotKey[num]) != 0)
				{
					Aimbot(screenWidth, screenHeight);
				}
				else if (currentBHAOffset != -999f)
				{
					currentBHAOffset = -999f;
				}
				Thread.Sleep(1);
			}
		}).Start();
	}

	private void TriggerBot()
	{
		if (Config.AppleStatus)
		{
			int num = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_X, Config.currentMode));
			int num2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_Y, Config.currentMode));
			int num3 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.TRIGGER_FOVX, Config.currentMode));
			int num4 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.TRIGGER_FOVY, Config.currentMode));
			Point[] result = PixelSearch(new Rectangle((num - num4) / 2, (num2 - num3) / 2, num4, num3)).GetAwaiter().GetResult();
			if (result != null && result.Length != 0)
			{
				ArduinoAdsMove(0, 0, true);
			}
		}
	}

	private void NewRecoilSystem()
	{
		if (!Config.AimbotStatus)
		{
			return;
		}
		if (!Config.SHITRECStatus)
		{
			if (_rcsCurrent != 0f)
			{
				UpdateRecoilCurrent(0f);
			}
			if (_holdingAimKey)
			{
				_holdingAimKey = false;
				_rcsHoldingTime = 0;
			}
		}
		else if (GetAsyncKeyState(1) != 0)
		{
			_holdingAimKey = true;
			_rcsHoldingTime++;
			if ((_rcsHoldingTime >= Convert.ToInt32(Config.GetConfig(Settings.CONFIG.RCS_HOLDINGTIME, Config.currentMode))) && ((_rcsCurrent <= Convert.ToInt32(Config.GetConfig(Settings.CONFIG.RCS_MAX_RECOIL, Config.currentMode))) ? true : false))
			{
				float num = float.Parse(Config.GetConfig(Settings.CONFIG.RCS_SPEED, Config.currentMode));
				int num2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.RCS_MULTIPLIER, Config.currentMode));
				float num3 = num;
				UpdateRecoilCurrent(_rcsCurrent + (float)num2 * num3);
			}
		}
		else if (_holdingAimKey)
		{
			_holdingAimKey = false;
			_rcsHoldingTime = 0;
			UpdateRecoilCurrent(0f);
		}
	}

	private void UpdateRecoilCurrent(float update)
	{
		_rcsCurrent = update;
	}

	private void AimAssist(int screenWidth, int screenHeight)
	{
		int num = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.ASSIST_FOV_X, Config.currentMode));
		int num2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.ASSIST_FOV_Y, Config.currentMode));
		int num3 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.ASSIST_MAX_COUNT, Config.currentMode));
		int num4 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.ASSIST_HEADSHOT_OFFSET, Config.currentMode));
		int maxValue = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.ASSIST_JITTER, Config.currentMode));
		float num5 = float.Parse(Config.GetConfig(Settings.CONFIG.ASSIST_SENS, Config.currentMode));
		Point[] result = PixelSearch(new Rectangle((screenWidth - num2) / 2, (screenHeight - num) / 2, num2, num)).GetAwaiter().GetResult();
		if (result == null || result.Length == 0)
		{
			return;
		}
		Point[] array = result.OrderBy((Point t) => t.Y).ToArray();
		List<Vector2> list = new List<Vector2>();
		float num6 = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			Vector2 current = new Vector2(array[i].X, array[i].Y);
			if (num6 > current.Y)
			{
				continue;
			}
			num6 = current.Y;
			if (list.Where((Vector2 t) => (t - current).Length() < 60f || Math.Abs(t.X - current.X) < 60f).Count() < 1)
			{
				list.Add(current);
				if (list.Count > num3)
				{
					break;
				}
			}
		}
		Vector2 vector = (from t in list
			select t - new Vector2(screenWidth / 2, screenHeight / 2) into t
			orderby t.Length()
			select t).FirstOrDefault() + new Vector2(1f, num4 + Convert.ToInt32(_rcsCurrent));
		Random random = new Random();
		float num7 = 1f - (float)random.Next(maxValue) / 100f;
		int xDelta = Convert.ToInt32(vector.X / num5 * num7);
		int yDelta = Convert.ToInt32(vector.Y / num5 * num7);
		ArduinoAdsMove(xDelta, yDelta, false);
	}

	private void Aimbot(int screenWidth, int screenHeight)
	{
		if (!Config.AimbotStatus)
		{
			return;
		}
		int num = 5;
		int num2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.AIMBOT_HEADSHOT_OFFSET, Config.currentMode));
		_ = (float)(float.Parse(Config.GetConfig(Settings.CONFIG.AIMBOT_SPEED_X, Config.currentMode)) / 100);
		_ = (float)(float.Parse(Config.GetConfig(Settings.CONFIG.AIMBOT_SPEED_Y, Config.currentMode)) / 100);
		int num3 = 5;
		float num4 = float.Parse(Config.GetConfig(Settings.CONFIG.AIMBOT_SENS, Config.currentMode));
		int num5 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.AIMBOT_FOV_X, Config.currentMode));
		int num6 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.AIMBOT_FOV_Y, Config.currentMode));
		bool flag = Convert.ToBoolean(Convert.ToInt32(Config.GetConfig(Settings.CONFIG.BHA_ENABLED, Config.currentMode)));
		bool flag2 = Convert.ToBoolean(Convert.ToInt32(Config.GetConfig(Settings.CONFIG.AIMBOT_RANDOM_SMOOTH, Config.currentMode)));
		bool shot = false;
		Point[] result = PixelSearch(new Rectangle((screenWidth - num6) / 2, (screenHeight - num5) / 2, num6, num5)).GetAwaiter().GetResult();
		if (result == null || result.Length == 0)
		{
			return;
		}
		Point[] array = result.OrderBy((Point t) => t.Y).ToArray();
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < array.Length; i++)
		{
			Vector2 current = new Vector2(array[i].X, array[i].Y);
			if (list.Where((Vector2 t) => (t - current).Length() < 60f || Math.Abs(t.X - current.X) < 60f).Count() < 1)
			{
				list.Add(current);
				if (list.Count > num)
				{
					break;
				}
			}
		}
		if (flag)
		{
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			float num10 =0;
			float num11 = num10;
			if (currentBHAOffset == -999f)
			{
				currentBHAOffset = num7;
			}
			if (currentBHAOffset > (float)num8)
			{
				currentBHAOffset -= (float)num9 * num11;
			}
			Random random = new Random();
			Vector2 vector = (from t in list
				select t - new Vector2(screenWidth / 2, screenHeight / 2) into t
				orderby t.Length()
				select t).FirstOrDefault() + new Vector2(1f, Convert.ToInt32(currentBHAOffset) + Convert.ToInt32(_rcsCurrent));
			int num12 = num3;
			if (flag2)
			{
				num12 = random.Next(num3);
			}
			float num13 = 1f - (float)num12 / 100f;
			int xDelta = Convert.ToInt32(vector.X / num4 * num13);
			int yDelta = Convert.ToInt32(vector.Y / num4 * num13);
			ArduinoAdsMove(xDelta, yDelta, shot);
		}
		else
		{
			Random random2 = new Random();
			Vector2 vector2 = (from t in list
				select t - new Vector2(screenWidth / 2, screenHeight / 2) into t
				orderby t.Length()
				select t).FirstOrDefault() + new Vector2(1f, num2 + Convert.ToInt32(_rcsCurrent));
			int num14 = num3;
			if (flag2)
			{
				num14 = random2.Next(num3);
			}
			float num15 = 1f - (float)num14 / 100f;
			int xDelta2 = Convert.ToInt32(vector2.X / num4 * num15);
			int yDelta2 = Convert.ToInt32(vector2.Y / num4 * num15);
			ArduinoAdsMove(xDelta2, yDelta2, shot);
		}
	}

	private void FlickBot()
	{
		int screenWidth = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_X, Config.currentMode));
		int screenHeight = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.CHEAT_RESOLUTION_Y, Config.currentMode));
		int num = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.FLICK_FOV_X, Config.currentMode));
		int num2 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.FLICK_FOV_Y, Config.currentMode));
		Point[] result = PixelSearch(new Rectangle((screenWidth - num2) / 2, (screenHeight - num) / 2, num2, num)).GetAwaiter().GetResult();
		if (result == null || result.Length == 0)
		{
			return;
		}
		int num3 = 5;
		int num4 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.FLICK_HEADSHOT_OFFSET, Config.currentMode));
		int num5 = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.FLICK_FOLLOW_TIME, Config.currentMode));
		int maxValue = Convert.ToInt32(Config.GetConfig(Settings.CONFIG.FLICK_JITTER, Config.currentMode));
		float num6 = float.Parse(Config.GetConfig(Settings.CONFIG.FLICK_SENS, Config.currentMode));
		Point[] array = result.OrderBy((Point t) => t.Y).ToArray();
		List<Vector2> list = new List<Vector2>();
		float num7 = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			Vector2 current = new Vector2(array[i].X, array[i].Y);
			if (num7 > current.Y)
			{
				continue;
			}
			num7 = current.Y;
			if (list.Where((Vector2 t) => (t - current).Length() < 60f || Math.Abs(t.X - current.X) < 60f).Count() < 1)
			{
				list.Add(current);
				if (list.Count > num3)
				{
					break;
				}
			}
		}
		Vector2 vector = (from t in list
			select t - new Vector2(screenWidth / 2, screenHeight / 2) into t
			orderby t.Length()
			select t).FirstOrDefault() + new Vector2(1f, num4);
		if (num5 != 0 && followTimer <= num5)
		{
			followTimer++;
		}
		if (followTimer <= num5)
		{
			Random random = new Random();
			float num8 = 1f - (float)random.Next(maxValue) / 100f;
			int xDelta = Convert.ToInt32(vector.X / num6 * num8);
			int yDelta = Convert.ToInt32(vector.Y / num6 * num8);
			ArduinoAdsMove(xDelta, yDelta, false);
		}
	}

	private void ShowESP(Point[] l)
	{
		Point[] array = l.OrderBy(delegate(Point t)
		{
			Point point = t;
			return point.Y;
		}).ToArray();
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < array.Length; i++)
		{
			if (Config.ESPStatus)
			{
				int num = 60;
				int height = 160;
				Color color = Color.FromArgb(255, 0, 0);
				Pen pen = new Pen(color)
				{
					Width = 2f
				};
				 Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
				graphics.DrawRectangle(pen, array[i].X - num / 2, array[i].Y - 10, num, height);
			}
			Vector2 current = new Vector2(array[i].X, array[i].Y);
			if (!list.Where((Vector2 t) => (t - current).Length() < 60f || Math.Abs(t.X - current.X) < 60f).Any())
			{
				list.Add(current);
				if (list.Count > 0)
				{
					break;
				}
			}
		}
	}

	public void ArduinoAdsMove(int xDelta, int yDelta, bool shot)
	{
		try
		{
			if (shot)
			{
				if (DateTime.Now.Subtract(lastShot).TotalMilliseconds < Convert.ToInt32(Config.GetConfig(Settings.CONFIG.TRIGGER_DELAY, Config.currentMode)))
				{
					shot = false;
				}
				else
				{
					lastShot = DateTime.Now;
				}
			}
			MainScreen._leonardo.Move(xDelta, yDelta, shot);
		}
		catch (Exception)
		{
		}
	}

	public unsafe async Task<Point[]> PixelSearch(Rectangle rect)
	{
		
		{
			ArrayList arrayList = new ArrayList();
			if (rect.Width != 0 && rect.Height != 0)
			{
				Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
				if (monitor >= Screen.AllScreens.Length)
				{
					monitor = 0;
				}
				int left = Screen.AllScreens[monitor].Bounds.Left;
				int top = Screen.AllScreens[monitor].Bounds.Top;
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.CopyFromScreen(rect.X + left, rect.Y + top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
				}
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
				for (int i = 0; i < bitmapData.Height; i++)
				{
					byte* ptr = (byte*)(void*)bitmapData.Scan0 + i * bitmapData.Stride;
					for (int j = 0; j < bitmapData.Width; j++)
					{
						int red = ptr[j * 3 + 2] & 0xFF;
						int green = ptr[j * 3 + 1] & 0xFF;
						int blue = ptr[j * 3] & 0xFF;
						if (IsPurpleColor(red, green, blue))
						{
							arrayList.Add(new Point(j + rect.X, i + rect.Y));
						}
					}
				}
				bitmap.Dispose();
				return (Point[])arrayList.ToArray(typeof(Point));
			}
			return null;
		};
	}

	public bool IsPurpleColor(int red, int green, int blue)
	{
		if (green >= 170)
		{
			return false;
		}
		if (green >= 120)
		{
			return Math.Abs(red - blue) <= 8 && red - green >= 50 && blue - green >= 50 && red >= 105 && blue >= 105;
		}
		return Math.Abs(red - blue) <= 13 && red - green >= 60 && blue - green >= 60 && red >= 110 && blue >= 100;
	}
}
