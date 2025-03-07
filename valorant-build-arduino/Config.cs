// unknown.Config
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using unknown;

public class Config
{
	public static string version;

	public static bool AimbotStatus;

	public static bool ESPStatus;

	public static bool AppleStatus;

	public static bool SHITRECStatus;

	public static bool BlueBerryStatus;

	public static bool FovDraw;

	public static bool TriggerAimbot;

	public static string myHWID;

	public static int currentMode;

	private static IniFile configIni;

	public static List<ConfigContainer> value;

	public static string CPUID;

	public static string BIOSID;

	public static string BASEID;

	public static string DISKID;

	public static string VIDEOID;

	public static string MACID;

	public static dynamic GetConfigFile(Settings.CONFIG conf, int mode)
	{
		if (configIni.KeyExists(conf.ToString(), mode.ToString()))
		{
			return configIni.Read(conf.ToString(), mode.ToString());
		}
		configIni.Write(conf.ToString(), "0", mode.ToString());
		return 0;
	}

	public static dynamic GetConfig(Settings.CONFIG conf, int mode)
	{
		ConfigContainer configContainer = value.FirstOrDefault((ConfigContainer x) => x.config == conf && x.mode == mode);
		if (configContainer == null)
		{
			object configFile = GetConfigFile(conf, mode);
			value.Add(new ConfigContainer
			{
				config = conf,
				mode = mode,
				value = configFile
			});
			configContainer = value.FirstOrDefault((ConfigContainer x) => x.config == conf && x.mode == mode);
			if (configContainer == null)
			{
				return -1;
			}
		}
		return configContainer.value;
	}

	public static void SetConfig(Settings.CONFIG conf, int mode, dynamic val)
	{
		try
		{
			configIni.Write(conf.ToString(), val.ToString(), mode.ToString());
			ConfigContainer configContainer = value.FirstOrDefault((ConfigContainer x) => x.config == conf && x.mode == mode);
			if (configContainer != null)
			{
				configContainer.value = (object)val;
				return;
			}
			value.Add(new ConfigContainer
			{
				config = conf,
				mode = mode,
				value = val
			});
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: Can't set configuration '" + conf.ToString() + "' in mode '" + mode + "' " + ex.ToString());
		}
	}

	static Config()
	{
		version = "For Leonardo R3";
		AimbotStatus = false;
		ESPStatus = false;
		AppleStatus = false;
		SHITRECStatus = false;
		BlueBerryStatus = false;
		FovDraw = false;
		TriggerAimbot = false;
		myHWID = "";
		currentMode = 0;
		configIni = new IniFile("Config.ini");
		value = new List<ConfigContainer>();
		CPUID = "";
		BIOSID = "";
		BASEID = "";
		DISKID = "";
		VIDEOID = "";
		MACID = "";
	}
}
