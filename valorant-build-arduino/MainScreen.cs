// unknown.MainScreen
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MetroSet_UI.Child;
using MetroSet_UI.Components;
using MetroSet_UI.Controls;
using MetroSet_UI.Forms;
using unknown;

public class MainScreen : MetroSetForm
{
	public static bool _debug;

	private static DebugWindow _debugWindow;

	public static leonardo _leonardo;

	private static string consoleName;

	private IContainer components = null;

	private MetroSetTabControl SystemTabs;

	private MetroSetComboBox leonardoPorts;

	private MetroSetSetTabPage AimbotTab;

	private MetroSetLabel metroSetLabel10;

	private MetroSetLabel metroSetLabel9;

	private MetroSetComboBox AimbotKeyCombo;

	private MetroSetLabel metroSetLabel8;

	private MetroSetLabel metroSetLabel4;

	private MetroSetSwitch metroSetSwitch2;

	private MetroSetDivider metroSetDivider2;

	private MetroSetTextBox fovYField;

	private MetroSetTextBox fovXField;

	private MetroSetTextBox headshotOffsetField;

	private MetroSetLabel metroSetLabel13;

	private MetroSetButton updateButton;

	private MetroSetSetTabPage metroSetSetTabPage2;

	private MetroSetTextBox resolutionYField;

	private MetroSetTextBox resolutionXField;

	private MetroSetLabel resolutionX;

	private MetroSetButton UpdateRes;

	private MetroSetSetTabPage noRCSTab;

	private MetroSetLabel metroSetLabel3;

	private MetroSetSwitch metroSetSwitch3;

	private MetroSetDivider metroSetDivider1;

	private MetroSetTextBox maxRecoilField;

	private MetroSetTextBox recoilMultiplierField;

	private MetroSetLabel metroSetLabel6;

	private MetroSetLabel metroSetLabel17;

	private MetroSetButton updateRecoil;

	private MetroSetComboBox currentModeCombo;

	private MetroSetSetToolTip metroSetSetToolTip1;

	private MetroSetLabel metroSetLabel15;

	private MetroSetLabel metroSetLabel5;

	private MetroSetSetTabPage triggerTab;

	private MetroSetLabel metroSetLabel33;

	private MetroSetSwitch AppleSwitch;

	private MetroSetDivider metroSetDivider4;

	private MetroSetTextBox triggerDelay;

	private MetroSetLabel metroSetLabel34;

	private MetroSetComboBox metroSetComboBox1;

	private MetroSetLabel metroSetLabel35;

	private MetroSetButton metroSetButton1;

	private MetroSetTextBox AppleFOVY;

	private MetroSetTextBox AppleFOVX;

	private MetroSetLabel metroSetLabel36;

	private MetroSetLabel metroSetLabel37;

	private MetroSetTextBox holdingTimeBox;

	private MetroSetLabel metroSetLabel47;

	private MetroSetLabel metroSetLabel18;

	private MetroSetTextBox rcsSpeed;

	private MetroSetComboBox metroSetComboBox2;

	private NotifyIcon notifyIcon1;

	private MetroSetTextBox ingameSens;

	private MetroSetLabel metroSetLabel11;

	private MetroSetLabel metroSetLabel46;
    private MetroSetLabel metroSetLabel7;
    private MetroSetLabel closeButton;
    private MetroSetSwitch rndSmooth;

	public MainScreen()
	{
		InitializeComponent();
		Text = consoleName;
	}

	private void Form1_Load(object sender, EventArgs e)
	{
        string _sundsaknm = "C9B9715E-C797-11EC-B067-5037B82D3E82";
        string _bsrnMbrs = "daoshdjoasjih4312848023740823";

        _debugWindow = new DebugWindow();
		_debugWindow.Enabled = true;
		_debugWindow.Hide();
		HotKeySystem();
		string[] portNames = SerialPort.GetPortNames();
		string[] array = portNames;
		foreach (string item in array)
		{
			leonardoPorts.Items.Add(item);
		}

		Screen[] allScreens = Screen.AllScreens;
		for (int j = 0; j < allScreens.Length; j++)
		{
			metroSetComboBox2.Items.Add($"Monitor [{j}]");
		}
		new PixelSearcher();
		Config.currentMode = Convert.ToInt32(Config.GetConfigFile(Settings.CONFIG.CHEAT_CURRENT_MODE, -1));
		LoadConfigInFields(Config.currentMode);
    }

    public string getUUID()
    {
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "CMD.exe";
        startInfo.Arguments = "/C wmic csproduct get UUID";
        process.StartInfo = startInfo;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        process.WaitForExit();
        string output = process.StandardOutput.ReadToEnd();
        return output;
    }

    public string getBiosSerialNumber()
    {
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "CMD.exe";
        startInfo.Arguments = "/C wmic bios get serialnumber";
        process.StartInfo = startInfo;
        
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        process.WaitForExit();
        string output = process.StandardOutput.ReadToEnd();
        return output;
    }

    public void LoadConfigInFields(int currentMode)
	{
        try
        {
            resolutionXField.Text = Config.GetConfigFile(Settings.CONFIG.CHEAT_RESOLUTION_X, currentMode).ToString();
            resolutionYField.Text = Config.GetConfigFile(Settings.CONFIG.CHEAT_RESOLUTION_Y, currentMode).ToString();
            recoilMultiplierField.Text = Config.GetConfigFile(Settings.CONFIG.RCS_MULTIPLIER, currentMode).ToString();
            maxRecoilField.Text = Config.GetConfigFile(Settings.CONFIG.RCS_MAX_RECOIL, currentMode).ToString();
            AimbotKeyCombo.SelectedIndex = Convert.ToInt32(Config.GetConfigFile(Settings.CONFIG.AIMBOT_HOTKEY, currentMode));
            metroSetComboBox1.SelectedIndex = Convert.ToInt32(Config.GetConfigFile(Settings.CONFIG.TRIGGER_HOTKEY, currentMode));
            fovXField.Text = Config.GetConfigFile(Settings.CONFIG.AIMBOT_FOV_X, currentMode).ToString();
            fovYField.Text = Config.GetConfigFile(Settings.CONFIG.AIMBOT_FOV_Y, currentMode).ToString();
            headshotOffsetField.Text = Config.GetConfigFile(Settings.CONFIG.AIMBOT_HEADSHOT_OFFSET, currentMode).ToString();
            holdingTimeBox.Text = Config.GetConfigFile(Settings.CONFIG.RCS_HOLDINGTIME, currentMode).ToString();
            AppleFOVX.Text = Config.GetConfigFile(Settings.CONFIG.TRIGGER_FOVX, currentMode).ToString();
            AppleFOVY.Text = Config.GetConfigFile(Settings.CONFIG.TRIGGER_FOVY, currentMode).ToString();
            rcsSpeed.Text = Config.GetConfigFile(Settings.CONFIG.RCS_SPEED, currentMode).ToString();
            ingameSens.Text = Config.GetConfigFile(Settings.CONFIG.AIMBOT_SENS, currentMode).ToString();
            int num = Convert.ToInt32(Config.GetConfigFile(Settings.CONFIG.CHEAT_MONITOR, currentMode));
            if (Screen.AllScreens.Length >= num)
            {
                metroSetComboBox2.SelectedIndex = num;
                PixelSearcher.monitor = num;
            }
            else
            {
                PixelSearcher.monitor = 0;
            }
            currentModeCombo.SelectedIndex = currentMode;
        }
        catch
        {
            MessageBox.Show("Error while loading config.\n\nPlease contact me on discord: @zeushappy#3503", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

	private void MainScreen_Move(object sender, EventArgs e)
	{
		if (_debugWindow != null && _debugWindow.Enabled)
		{
			_debugWindow.Location = new Point(base.Location.X + 460, base.Location.Y);
		}
	}

	private void metroSetSwitch2_SwitchedChanged(object sender)
	{
		if (!_leonardo.leonardoOpened)
		{
			metroSetSwitch2.Switched = false;
			return;
		}
		Config.AimbotStatus = !Config.AimbotStatus;
		if (_debug)
		{
		}
	}

	private void leonardoPorts_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (_debug)
		{
		}
		_leonardo = new leonardo(leonardoPorts.SelectedItem.ToString());
	}

	private void AimbotKeyCombo_SelectedIndexChanged(object sender, EventArgs e)
	{
		Config.SetConfig(val: AimbotKeyCombo.SelectedIndex.ToString(), conf: Settings.CONFIG.AIMBOT_HOTKEY, mode: Config.currentMode);
	}

	private void updateButton_Click(object sender, EventArgs e)
	{
        if (fovXField.Text == "" || fovYField.Text == "")
        {
            MessageBox.Show("Hey Martins, você não pode deixar o campo de FOV vazio!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (metroSetSwitch2.Switched){
		    UpdateAimbotConfig();
        }
	}

	public void UpdateAimbotConfig()
	{
		Config.SetConfig(Settings.CONFIG.AIMBOT_FOV_X, Config.currentMode, fovXField.Text.ToString());
		Config.SetConfig(Settings.CONFIG.AIMBOT_FOV_Y, Config.currentMode, fovYField.Text.ToString());
		Config.SetConfig(Settings.CONFIG.AIMBOT_HEADSHOT_OFFSET, Config.currentMode, headshotOffsetField.Text.ToString());
		Config.SetConfig(Settings.CONFIG.AIMBOT_SENS, Config.currentMode, ingameSens.Text.ToString());
	}

	private void UpdateRes_Click(object sender, EventArgs e)
	{
		UpdateResolutionConfig();
	}

	private void updateRecoil_Click(object sender, EventArgs e)
	{
		UpdateRecoilConfig();
	}

	public void UpdateResolutionConfig()
	{
		Config.SetConfig(Settings.CONFIG.CHEAT_RESOLUTION_X, Config.currentMode, resolutionXField.Text);
		Config.SetConfig(Settings.CONFIG.CHEAT_RESOLUTION_Y, Config.currentMode, resolutionYField.Text);
	}

	public void UpdateRecoilConfig()
	{
		Config.SetConfig(Settings.CONFIG.RCS_MULTIPLIER, Config.currentMode, recoilMultiplierField.Text);
		Config.SetConfig(Settings.CONFIG.RCS_MAX_RECOIL, Config.currentMode, maxRecoilField.Text);
		Config.SetConfig(Settings.CONFIG.RCS_HOLDINGTIME, Config.currentMode, holdingTimeBox.Text);
		Config.SetConfig(Settings.CONFIG.RCS_SPEED, Config.currentMode, rcsSpeed.Text);
	}

	private void metroSetSwitch3_SwitchedChanged(object sender)
	{
		Config.SHITRECStatus = !Config.SHITRECStatus;
	}

	private void currentModeCombo_SelectedIndexChanged(object sender, EventArgs e)
	{
		UpdateCurrentMode(Config.currentMode = currentModeCombo.SelectedIndex);
	}

	public void UpdateCurrentMode(int id)
	{
		Invoke((MethodInvoker)delegate
		{
			currentModeCombo.SelectedIndex = id;
		});
		Invoke((MethodInvoker)delegate
		{
			LoadConfigInFields(id);
		});
		UpdateAimbotConfig();
		UpdateResolutionConfig();
		UpdateRecoilConfig();
	}

	private void HotKeySystem()
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			while (true)
			{
				if (GetAsyncKeyState(112) == 0)
				{
					if (GetAsyncKeyState(113) != 0)
					{
						if (Config.currentMode == 1)
						{
							continue;
						}
						Config.currentMode = 1;
						Console.Beep(1000, 100);
						UpdateCurrentMode(1);
					}
					else if (GetAsyncKeyState(114) != 0)
					{
						if (Config.currentMode == 2)
						{
							continue;
						}
						Config.currentMode = 2;
						Console.Beep(1500, 100);
						UpdateCurrentMode(2);
					}
				}
				else
				{
					if (Config.currentMode == 0)
					{
						continue;
					}
					Config.currentMode = 0;
					Console.Beep(800, 100);
					UpdateCurrentMode(0);
				}
				Thread.Sleep(150);
			}
		});
		thread.Start();
	}

	[DllImport("user32.dll")]
	private static extern short GetAsyncKeyState(int vKey);

	private void BlueBerrySwitch_SwitchedChanged(object sender)
	{
		Config.BlueBerryStatus = !Config.BlueBerryStatus;
	}

	private void AppleSwitch_SwitchedChanged(object sender)
	{
		Config.AppleStatus = AppleSwitch.Switched;
	}

	private void metroSetComboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		Config.SetConfig(val: metroSetComboBox1.SelectedIndex.ToString(), conf: Settings.CONFIG.TRIGGER_HOTKEY, mode: Config.currentMode);
	}

	private void metroSetButton1_Click(object sender, EventArgs e)
	{
		Config.SetConfig(Settings.CONFIG.TRIGGER_DELAY, Config.currentMode, triggerDelay.Text);
		Config.SetConfig(Settings.CONFIG.TRIGGER_FOVX, Config.currentMode, AppleFOVX.Text);
		Config.SetConfig(Settings.CONFIG.TRIGGER_FOVY, Config.currentMode, AppleFOVY.Text);
	}

	private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
	{
		Application.Exit();
		Environment.Exit(Environment.ExitCode);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		Graphics graphics = CreateGraphics();
		Pen pen = new Pen(Color.Red);
		new SolidBrush(Color.Red);
		graphics.DrawEllipse(pen, 0, 0, 80, 35);
	}

	private void metroSetComboBox2_SelectedIndexChanged(object sender, EventArgs e)
	{
		PixelSearcher.monitor = metroSetComboBox2.SelectedIndex;
		Config.SetConfig(Settings.CONFIG.CHEAT_MONITOR, Config.currentMode, metroSetComboBox2.SelectedIndex);
	}

	private void MainScreen_Resize(object sender, EventArgs e)
	{
	}

	private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
	}

	private void rndSmooth_SwitchedChanged(object sender)
	{
		Config.SetConfig(Settings.CONFIG.AIMBOT_RANDOM_SMOOTH, Config.currentMode, Convert.ToInt32(rndSmooth.Switched).ToString());
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
            this.components = new System.ComponentModel.Container();
            MetroSet_UI.Components.StyleManager styleManager1;
            this.SystemTabs = new MetroSet_UI.Controls.MetroSetTabControl();
            this.AimbotTab = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.metroSetLabel46 = new MetroSet_UI.Controls.MetroSetLabel();
            this.rndSmooth = new MetroSet_UI.Controls.MetroSetSwitch();
            this.ingameSens = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel11 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel15 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel5 = new MetroSet_UI.Controls.MetroSetLabel();
            this.updateButton = new MetroSet_UI.Controls.MetroSetButton();
            this.headshotOffsetField = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel13 = new MetroSet_UI.Controls.MetroSetLabel();
            this.fovYField = new MetroSet_UI.Controls.MetroSetTextBox();
            this.fovXField = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel10 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel9 = new MetroSet_UI.Controls.MetroSetLabel();
            this.AimbotKeyCombo = new MetroSet_UI.Controls.MetroSetComboBox();
            this.metroSetLabel8 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel4 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetSwitch2 = new MetroSet_UI.Controls.MetroSetSwitch();
            this.metroSetDivider2 = new MetroSet_UI.Controls.MetroSetDivider();
            this.noRCSTab = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.metroSetLabel18 = new MetroSet_UI.Controls.MetroSetLabel();
            this.rcsSpeed = new MetroSet_UI.Controls.MetroSetTextBox();
            this.holdingTimeBox = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel47 = new MetroSet_UI.Controls.MetroSetLabel();
            this.updateRecoil = new MetroSet_UI.Controls.MetroSetButton();
            this.maxRecoilField = new MetroSet_UI.Controls.MetroSetTextBox();
            this.recoilMultiplierField = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel6 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel17 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel3 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetSwitch3 = new MetroSet_UI.Controls.MetroSetSwitch();
            this.metroSetDivider1 = new MetroSet_UI.Controls.MetroSetDivider();
            this.triggerTab = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.AppleFOVY = new MetroSet_UI.Controls.MetroSetTextBox();
            this.AppleFOVX = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel36 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel37 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetButton1 = new MetroSet_UI.Controls.MetroSetButton();
            this.metroSetComboBox1 = new MetroSet_UI.Controls.MetroSetComboBox();
            this.metroSetLabel35 = new MetroSet_UI.Controls.MetroSetLabel();
            this.triggerDelay = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetLabel34 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetLabel33 = new MetroSet_UI.Controls.MetroSetLabel();
            this.AppleSwitch = new MetroSet_UI.Controls.MetroSetSwitch();
            this.metroSetDivider4 = new MetroSet_UI.Controls.MetroSetDivider();
            this.metroSetSetTabPage2 = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.metroSetLabel7 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetComboBox2 = new MetroSet_UI.Controls.MetroSetComboBox();
            this.UpdateRes = new MetroSet_UI.Controls.MetroSetButton();
            this.resolutionYField = new MetroSet_UI.Controls.MetroSetTextBox();
            this.resolutionXField = new MetroSet_UI.Controls.MetroSetTextBox();
            this.resolutionX = new MetroSet_UI.Controls.MetroSetLabel();
            this.leonardoPorts = new MetroSet_UI.Controls.MetroSetComboBox();
            this.currentModeCombo = new MetroSet_UI.Controls.MetroSetComboBox();
            this.metroSetSetToolTip1 = new MetroSet_UI.Components.MetroSetSetToolTip();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.closeButton = new MetroSet_UI.Controls.MetroSetLabel();
            styleManager1 = new MetroSet_UI.Components.StyleManager();
            this.SystemTabs.SuspendLayout();
            this.AimbotTab.SuspendLayout();
            this.noRCSTab.SuspendLayout();
            this.triggerTab.SuspendLayout();
            this.metroSetSetTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            styleManager1.CustomTheme = "C:\\Users\\user\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            styleManager1.MetroForm = this;
            styleManager1.Style = MetroSet_UI.Enums.Style.Light;
            styleManager1.ThemeAuthor = null;
            styleManager1.ThemeName = null;
            // 
            // SystemTabs
            // 
            this.SystemTabs.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            this.SystemTabs.AnimateTime = 200;
            this.SystemTabs.BackgroundColor = System.Drawing.Color.White;
            this.SystemTabs.Controls.Add(this.AimbotTab);
            this.SystemTabs.Controls.Add(this.noRCSTab);
            this.SystemTabs.Controls.Add(this.triggerTab);
            this.SystemTabs.Controls.Add(this.metroSetSetTabPage2);
            this.SystemTabs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SystemTabs.IsDerivedStyle = true;
            this.SystemTabs.ItemSize = new System.Drawing.Size(100, 38);
            this.SystemTabs.Location = new System.Drawing.Point(15, 65);
            this.SystemTabs.Name = "SystemTabs";
            this.SystemTabs.SelectedIndex = 2;
            this.SystemTabs.SelectedTextColor = System.Drawing.Color.White;
            this.SystemTabs.Size = new System.Drawing.Size(432, 346);
            this.SystemTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.SystemTabs.Speed = 100;
            this.SystemTabs.Style = MetroSet_UI.Enums.Style.Light;
            this.SystemTabs.StyleManager = styleManager1;
            this.SystemTabs.TabIndex = 1;
            this.SystemTabs.ThemeAuthor = null;
            this.SystemTabs.ThemeName = null;
            this.SystemTabs.UnselectedTextColor = System.Drawing.Color.Gray;
            this.SystemTabs.UseAnimation = false;
            this.SystemTabs.SelectedIndexChanged += new System.EventHandler(this.SystemTabs_SelectedIndexChanged);
            // 
            // AimbotTab
            // 
            this.AimbotTab.BaseColor = System.Drawing.Color.White;
            this.AimbotTab.Controls.Add(this.metroSetLabel46);
            this.AimbotTab.Controls.Add(this.rndSmooth);
            this.AimbotTab.Controls.Add(this.ingameSens);
            this.AimbotTab.Controls.Add(this.metroSetLabel11);
            this.AimbotTab.Controls.Add(this.metroSetLabel15);
            this.AimbotTab.Controls.Add(this.metroSetLabel5);
            this.AimbotTab.Controls.Add(this.updateButton);
            this.AimbotTab.Controls.Add(this.headshotOffsetField);
            this.AimbotTab.Controls.Add(this.metroSetLabel13);
            this.AimbotTab.Controls.Add(this.fovYField);
            this.AimbotTab.Controls.Add(this.fovXField);
            this.AimbotTab.Controls.Add(this.metroSetLabel10);
            this.AimbotTab.Controls.Add(this.metroSetLabel9);
            this.AimbotTab.Controls.Add(this.AimbotKeyCombo);
            this.AimbotTab.Controls.Add(this.metroSetLabel8);
            this.AimbotTab.Controls.Add(this.metroSetLabel4);
            this.AimbotTab.Controls.Add(this.metroSetSwitch2);
            this.AimbotTab.Controls.Add(this.metroSetDivider2);
            this.AimbotTab.Font = null;
            this.AimbotTab.ImageIndex = 0;
            this.AimbotTab.ImageKey = null;
            this.AimbotTab.IsDerivedStyle = true;
            this.AimbotTab.Location = new System.Drawing.Point(4, 42);
            this.AimbotTab.Name = "AimbotTab";
            this.AimbotTab.Size = new System.Drawing.Size(424, 300);
            this.AimbotTab.Style = MetroSet_UI.Enums.Style.Light;
            this.AimbotTab.StyleManager = styleManager1;
            this.AimbotTab.TabIndex = 1;
            this.AimbotTab.Text = "Aimbot";
            this.AimbotTab.ThemeAuthor = "Narwin";
            this.AimbotTab.ThemeName = "MetroLite";
            this.AimbotTab.ToolTipText = null;
            // 
            // metroSetLabel46
            // 
            this.metroSetLabel46.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel46.IsDerivedStyle = true;
            this.metroSetLabel46.Location = new System.Drawing.Point(165, 121);
            this.metroSetLabel46.Name = "metroSetLabel46";
            this.metroSetLabel46.Size = new System.Drawing.Size(97, 23);
            this.metroSetLabel46.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel46.StyleManager = styleManager1;
            this.metroSetLabel46.TabIndex = 50;
            this.metroSetLabel46.Text = "Random Jitter";
            this.metroSetLabel46.ThemeAuthor = null;
            this.metroSetLabel46.ThemeName = null;
            // 
            // rndSmooth
            // 
            this.rndSmooth.BackColor = System.Drawing.Color.Transparent;
            this.rndSmooth.BackgroundColor = System.Drawing.Color.Empty;
            this.rndSmooth.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(159)))), ((int)(((byte)(147)))));
            this.rndSmooth.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.rndSmooth.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.rndSmooth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rndSmooth.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.rndSmooth.DisabledCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.rndSmooth.DisabledUnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.rndSmooth.IsDerivedStyle = true;
            this.rndSmooth.Location = new System.Drawing.Point(178, 147);
            this.rndSmooth.Name = "rndSmooth";
            this.rndSmooth.Size = new System.Drawing.Size(58, 22);
            this.rndSmooth.Style = MetroSet_UI.Enums.Style.Light;
            this.rndSmooth.StyleManager = null;
            this.rndSmooth.Switched = false;
            this.rndSmooth.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.rndSmooth.TabIndex = 49;
            this.rndSmooth.Text = "metroSetSwitch1";
            this.rndSmooth.ThemeAuthor = "Narwin";
            this.rndSmooth.ThemeName = "MetroLite";
            this.rndSmooth.UnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.rndSmooth.SwitchedChanged += new MetroSet_UI.Controls.MetroSetSwitch.SwitchedChangedEventHandler(this.rndSmooth_SwitchedChanged);
            // 
            // ingameSens
            // 
            this.ingameSens.AutoCompleteCustomSource = null;
            this.ingameSens.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.ingameSens.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.ingameSens.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.ingameSens.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ingameSens.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.ingameSens.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.ingameSens.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ingameSens.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ingameSens.Image = null;
            this.ingameSens.IsDerivedStyle = true;
            this.ingameSens.Lines = null;
            this.ingameSens.Location = new System.Drawing.Point(178, 79);
            this.ingameSens.MaxLength = 2;
            this.ingameSens.Multiline = false;
            this.ingameSens.Name = "ingameSens";
            this.ingameSens.ReadOnly = false;
            this.ingameSens.Size = new System.Drawing.Size(66, 30);
            this.ingameSens.Style = MetroSet_UI.Enums.Style.Light;
            this.ingameSens.StyleManager = styleManager1;
            this.ingameSens.TabIndex = 46;
            this.ingameSens.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ingameSens.ThemeAuthor = null;
            this.ingameSens.ThemeName = null;
            this.ingameSens.UseSystemPasswordChar = false;
            this.ingameSens.WatermarkText = "";
            this.ingameSens.TextChanged += new System.EventHandler(this.ingameSens_TextChanged);
            this.ingameSens.Click += new System.EventHandler(this.ingameSens_Click);
            // 
            // metroSetLabel11
            // 
            this.metroSetLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel11.IsDerivedStyle = true;
            this.metroSetLabel11.Location = new System.Drawing.Point(181, 55);
            this.metroSetLabel11.Name = "metroSetLabel11";
            this.metroSetLabel11.Size = new System.Drawing.Size(61, 23);
            this.metroSetLabel11.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel11.StyleManager = styleManager1;
            this.metroSetLabel11.TabIndex = 45;
            this.metroSetLabel11.Text = "Smooth";
            this.metroSetLabel11.ThemeAuthor = null;
            this.metroSetLabel11.ThemeName = null;
            // 
            // metroSetLabel15
            // 
            this.metroSetLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroSetLabel15.IsDerivedStyle = true;
            this.metroSetLabel15.Location = new System.Drawing.Point(305, 123);
            this.metroSetLabel15.Name = "metroSetLabel15";
            this.metroSetLabel15.Size = new System.Drawing.Size(121, 17);
            this.metroSetLabel15.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel15.StyleManager = styleManager1;
            this.metroSetLabel15.TabIndex = 34;
            this.metroSetLabel15.Text = "Decrease = More Head";
            this.metroSetLabel15.ThemeAuthor = null;
            this.metroSetLabel15.ThemeName = null;
            // 
            // metroSetLabel5
            // 
            this.metroSetLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroSetLabel5.IsDerivedStyle = true;
            this.metroSetLabel5.Location = new System.Drawing.Point(309, 110);
            this.metroSetLabel5.Name = "metroSetLabel5";
            this.metroSetLabel5.Size = new System.Drawing.Size(111, 23);
            this.metroSetLabel5.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel5.StyleManager = styleManager1;
            this.metroSetLabel5.TabIndex = 33;
            this.metroSetLabel5.Text = "Increase = More Body";
            this.metroSetLabel5.ThemeAuthor = null;
            this.metroSetLabel5.ThemeName = null;
            // 
            // updateButton
            // 
            this.updateButton.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateButton.DisabledForeColor = System.Drawing.Color.Gray;
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateButton.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.updateButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.updateButton.HoverTextColor = System.Drawing.Color.White;
            this.updateButton.IsDerivedStyle = true;
            this.updateButton.Location = new System.Drawing.Point(317, 143);
            this.updateButton.Name = "updateButton";
            this.updateButton.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateButton.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateButton.NormalTextColor = System.Drawing.Color.White;
            this.updateButton.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.updateButton.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.updateButton.PressTextColor = System.Drawing.Color.White;
            this.updateButton.Size = new System.Drawing.Size(93, 32);
            this.updateButton.Style = MetroSet_UI.Enums.Style.Light;
            this.updateButton.StyleManager = styleManager1;
            this.updateButton.TabIndex = 29;
            this.updateButton.Text = "Atualizar";
            this.updateButton.ThemeAuthor = null;
            this.updateButton.ThemeName = null;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // headshotOffsetField
            // 
            this.headshotOffsetField.AutoCompleteCustomSource = null;
            this.headshotOffsetField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.headshotOffsetField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.headshotOffsetField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.headshotOffsetField.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.headshotOffsetField.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.headshotOffsetField.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.headshotOffsetField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.headshotOffsetField.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.headshotOffsetField.Image = null;
            this.headshotOffsetField.IsDerivedStyle = true;
            this.headshotOffsetField.Lines = null;
            this.headshotOffsetField.Location = new System.Drawing.Point(317, 77);
            this.headshotOffsetField.MaxLength = 2;
            this.headshotOffsetField.Multiline = false;
            this.headshotOffsetField.Name = "headshotOffsetField";
            this.headshotOffsetField.ReadOnly = false;
            this.headshotOffsetField.Size = new System.Drawing.Size(91, 30);
            this.headshotOffsetField.Style = MetroSet_UI.Enums.Style.Light;
            this.headshotOffsetField.StyleManager = styleManager1;
            this.headshotOffsetField.TabIndex = 24;
            this.headshotOffsetField.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.headshotOffsetField.ThemeAuthor = "Narwin";
            this.headshotOffsetField.ThemeName = "MetroDark";
            this.headshotOffsetField.UseSystemPasswordChar = false;
            this.headshotOffsetField.WatermarkText = "";
            // 
            // metroSetLabel13
            // 
            this.metroSetLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel13.IsDerivedStyle = true;
            this.metroSetLabel13.Location = new System.Drawing.Point(319, 53);
            this.metroSetLabel13.Name = "metroSetLabel13";
            this.metroSetLabel13.Size = new System.Drawing.Size(89, 23);
            this.metroSetLabel13.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel13.StyleManager = styleManager1;
            this.metroSetLabel13.TabIndex = 23;
            this.metroSetLabel13.Text = "Head Offset";
            this.metroSetLabel13.ThemeAuthor = null;
            this.metroSetLabel13.ThemeName = null;
            // 
            // fovYField
            // 
            this.fovYField.AutoCompleteCustomSource = null;
            this.fovYField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.fovYField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.fovYField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.fovYField.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.fovYField.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.fovYField.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.fovYField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.fovYField.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.fovYField.Image = null;
            this.fovYField.IsDerivedStyle = true;
            this.fovYField.Lines = null;
            this.fovYField.Location = new System.Drawing.Point(3, 143);
            this.fovYField.MaxLength = 3;
            this.fovYField.Multiline = false;
            this.fovYField.Name = "fovYField";
            this.fovYField.ReadOnly = false;
            this.fovYField.Size = new System.Drawing.Size(111, 30);
            this.fovYField.Style = MetroSet_UI.Enums.Style.Light;
            this.fovYField.StyleManager = styleManager1;
            this.fovYField.TabIndex = 22;
            this.fovYField.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.fovYField.ThemeAuthor = null;
            this.fovYField.ThemeName = null;
            this.fovYField.UseSystemPasswordChar = false;
            this.fovYField.WatermarkText = "";
            // 
            // fovXField
            // 
            this.fovXField.AutoCompleteCustomSource = null;
            this.fovXField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.fovXField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.fovXField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.fovXField.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.fovXField.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.fovXField.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.fovXField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.fovXField.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.fovXField.Image = null;
            this.fovXField.IsDerivedStyle = true;
            this.fovXField.Lines = null;
            this.fovXField.Location = new System.Drawing.Point(3, 75);
            this.fovXField.MaxLength = 3;
            this.fovXField.Multiline = false;
            this.fovXField.Name = "fovXField";
            this.fovXField.ReadOnly = false;
            this.fovXField.Size = new System.Drawing.Size(111, 30);
            this.fovXField.Style = MetroSet_UI.Enums.Style.Light;
            this.fovXField.StyleManager = styleManager1;
            this.fovXField.TabIndex = 21;
            this.fovXField.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.fovXField.ThemeAuthor = null;
            this.fovXField.ThemeName = null;
            this.fovXField.UseSystemPasswordChar = false;
            this.fovXField.WatermarkText = "";
            // 
            // metroSetLabel10
            // 
            this.metroSetLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel10.IsDerivedStyle = true;
            this.metroSetLabel10.Location = new System.Drawing.Point(3, 117);
            this.metroSetLabel10.Name = "metroSetLabel10";
            this.metroSetLabel10.Size = new System.Drawing.Size(111, 23);
            this.metroSetLabel10.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel10.StyleManager = styleManager1;
            this.metroSetLabel10.TabIndex = 12;
            this.metroSetLabel10.Text = "FOV (Y)";
            this.metroSetLabel10.ThemeAuthor = null;
            this.metroSetLabel10.ThemeName = null;
            // 
            // metroSetLabel9
            // 
            this.metroSetLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel9.IsDerivedStyle = true;
            this.metroSetLabel9.Location = new System.Drawing.Point(3, 49);
            this.metroSetLabel9.Name = "metroSetLabel9";
            this.metroSetLabel9.Size = new System.Drawing.Size(111, 23);
            this.metroSetLabel9.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel9.StyleManager = styleManager1;
            this.metroSetLabel9.TabIndex = 11;
            this.metroSetLabel9.Text = "FOV (X)";
            this.metroSetLabel9.ThemeAuthor = null;
            this.metroSetLabel9.ThemeName = null;
            // 
            // AimbotKeyCombo
            // 
            this.AimbotKeyCombo.AllowDrop = true;
            this.AimbotKeyCombo.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.AimbotKeyCombo.BackColor = System.Drawing.Color.Transparent;
            this.AimbotKeyCombo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.AimbotKeyCombo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.AimbotKeyCombo.CausesValidation = false;
            this.AimbotKeyCombo.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.AimbotKeyCombo.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.AimbotKeyCombo.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.AimbotKeyCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.AimbotKeyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AimbotKeyCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.AimbotKeyCombo.FormattingEnabled = true;
            this.AimbotKeyCombo.IsDerivedStyle = true;
            this.AimbotKeyCombo.ItemHeight = 20;
            this.AimbotKeyCombo.Items.AddRange(new object[] {
            "Mouse 1",
            "Mouse 2",
            "Mouse 3",
            "Mouse 4",
            "Mouse 5",
            "Shift",
            "Alt",
            "CTRL",
            "Caps Lock"});
            this.AimbotKeyCombo.Location = new System.Drawing.Point(287, 3);
            this.AimbotKeyCombo.Name = "AimbotKeyCombo";
            this.AimbotKeyCombo.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.AimbotKeyCombo.SelectedItemForeColor = System.Drawing.Color.White;
            this.AimbotKeyCombo.Size = new System.Drawing.Size(121, 26);
            this.AimbotKeyCombo.Style = MetroSet_UI.Enums.Style.Light;
            this.AimbotKeyCombo.StyleManager = styleManager1;
            this.AimbotKeyCombo.TabIndex = 10;
            this.AimbotKeyCombo.ThemeAuthor = null;
            this.AimbotKeyCombo.ThemeName = null;
            this.AimbotKeyCombo.SelectedIndexChanged += new System.EventHandler(this.AimbotKeyCombo_SelectedIndexChanged);
            // 
            // metroSetLabel8
            // 
            this.metroSetLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel8.IsDerivedStyle = true;
            this.metroSetLabel8.Location = new System.Drawing.Point(226, 6);
            this.metroSetLabel8.Name = "metroSetLabel8";
            this.metroSetLabel8.Size = new System.Drawing.Size(55, 23);
            this.metroSetLabel8.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel8.StyleManager = styleManager1;
            this.metroSetLabel8.TabIndex = 9;
            this.metroSetLabel8.Text = "HotKey";
            this.metroSetLabel8.ThemeAuthor = null;
            this.metroSetLabel8.ThemeName = null;
            // 
            // metroSetLabel4
            // 
            this.metroSetLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel4.IsDerivedStyle = true;
            this.metroSetLabel4.Location = new System.Drawing.Point(66, 8);
            this.metroSetLabel4.Name = "metroSetLabel4";
            this.metroSetLabel4.Size = new System.Drawing.Size(100, 23);
            this.metroSetLabel4.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel4.StyleManager = styleManager1;
            this.metroSetLabel4.TabIndex = 6;
            this.metroSetLabel4.Text = "Aimbot";
            this.metroSetLabel4.ThemeAuthor = null;
            this.metroSetLabel4.ThemeName = null;
            // 
            // metroSetSwitch2
            // 
            this.metroSetSwitch2.BackColor = System.Drawing.Color.Transparent;
            this.metroSetSwitch2.BackgroundColor = System.Drawing.Color.Empty;
            this.metroSetSwitch2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(159)))), ((int)(((byte)(147)))));
            this.metroSetSwitch2.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetSwitch2.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.metroSetSwitch2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroSetSwitch2.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.metroSetSwitch2.DisabledCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetSwitch2.DisabledUnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.metroSetSwitch2.IsDerivedStyle = true;
            this.metroSetSwitch2.Location = new System.Drawing.Point(2, 7);
            this.metroSetSwitch2.Name = "metroSetSwitch2";
            this.metroSetSwitch2.Size = new System.Drawing.Size(58, 22);
            this.metroSetSwitch2.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetSwitch2.StyleManager = styleManager1;
            this.metroSetSwitch2.Switched = false;
            this.metroSetSwitch2.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.metroSetSwitch2.TabIndex = 5;
            this.metroSetSwitch2.Text = "AimbotSwitch";
            this.metroSetSwitch2.ThemeAuthor = null;
            this.metroSetSwitch2.ThemeName = null;
            this.metroSetSwitch2.UnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.metroSetSwitch2.SwitchedChanged += new MetroSet_UI.Controls.MetroSetSwitch.SwitchedChangedEventHandler(this.metroSetSwitch2_SwitchedChanged);
            // 
            // metroSetDivider2
            // 
            this.metroSetDivider2.IsDerivedStyle = true;
            this.metroSetDivider2.Location = new System.Drawing.Point(2, 34);
            this.metroSetDivider2.Name = "metroSetDivider2";
            this.metroSetDivider2.Orientation = MetroSet_UI.Enums.DividerStyle.Horizontal;
            this.metroSetDivider2.Size = new System.Drawing.Size(416, 4);
            this.metroSetDivider2.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetDivider2.StyleManager = styleManager1;
            this.metroSetDivider2.TabIndex = 4;
            this.metroSetDivider2.Text = "metroSetDivider2";
            this.metroSetDivider2.ThemeAuthor = null;
            this.metroSetDivider2.ThemeName = null;
            this.metroSetDivider2.Thickness = 1;
            // 
            // noRCSTab
            // 
            this.noRCSTab.BaseColor = System.Drawing.Color.White;
            this.noRCSTab.Controls.Add(this.metroSetLabel18);
            this.noRCSTab.Controls.Add(this.rcsSpeed);
            this.noRCSTab.Controls.Add(this.holdingTimeBox);
            this.noRCSTab.Controls.Add(this.metroSetLabel47);
            this.noRCSTab.Controls.Add(this.updateRecoil);
            this.noRCSTab.Controls.Add(this.maxRecoilField);
            this.noRCSTab.Controls.Add(this.recoilMultiplierField);
            this.noRCSTab.Controls.Add(this.metroSetLabel6);
            this.noRCSTab.Controls.Add(this.metroSetLabel17);
            this.noRCSTab.Controls.Add(this.metroSetLabel3);
            this.noRCSTab.Controls.Add(this.metroSetSwitch3);
            this.noRCSTab.Controls.Add(this.metroSetDivider1);
            this.noRCSTab.Font = null;
            this.noRCSTab.ImageIndex = 0;
            this.noRCSTab.ImageKey = null;
            this.noRCSTab.IsDerivedStyle = true;
            this.noRCSTab.Location = new System.Drawing.Point(4, 42);
            this.noRCSTab.Name = "noRCSTab";
            this.noRCSTab.Size = new System.Drawing.Size(424, 300);
            this.noRCSTab.Style = MetroSet_UI.Enums.Style.Light;
            this.noRCSTab.StyleManager = styleManager1;
            this.noRCSTab.TabIndex = 4;
            this.noRCSTab.Text = "No Recoil";
            this.noRCSTab.ThemeAuthor = "Narwin";
            this.noRCSTab.ThemeName = "MetroLite";
            this.noRCSTab.ToolTipText = null;
            // 
            // metroSetLabel18
            // 
            this.metroSetLabel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel18.IsDerivedStyle = true;
            this.metroSetLabel18.Location = new System.Drawing.Point(2, 54);
            this.metroSetLabel18.Name = "metroSetLabel18";
            this.metroSetLabel18.Size = new System.Drawing.Size(81, 23);
            this.metroSetLabel18.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel18.StyleManager = styleManager1;
            this.metroSetLabel18.TabIndex = 35;
            this.metroSetLabel18.Text = "Velocidade";
            this.metroSetLabel18.ThemeAuthor = null;
            this.metroSetLabel18.ThemeName = null;
            // 
            // rcsSpeed
            // 
            this.rcsSpeed.AutoCompleteCustomSource = null;
            this.rcsSpeed.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.rcsSpeed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.rcsSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.rcsSpeed.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.rcsSpeed.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.rcsSpeed.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.rcsSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rcsSpeed.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.rcsSpeed.Image = null;
            this.rcsSpeed.IsDerivedStyle = true;
            this.rcsSpeed.Lines = null;
            this.rcsSpeed.Location = new System.Drawing.Point(4, 78);
            this.rcsSpeed.MaxLength = 3;
            this.rcsSpeed.Multiline = false;
            this.rcsSpeed.Name = "rcsSpeed";
            this.rcsSpeed.ReadOnly = false;
            this.rcsSpeed.Size = new System.Drawing.Size(73, 30);
            this.rcsSpeed.Style = MetroSet_UI.Enums.Style.Light;
            this.rcsSpeed.StyleManager = styleManager1;
            this.rcsSpeed.TabIndex = 34;
            this.rcsSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.rcsSpeed.ThemeAuthor = null;
            this.rcsSpeed.ThemeName = null;
            this.rcsSpeed.UseSystemPasswordChar = false;
            this.rcsSpeed.WatermarkText = "";
            // 
            // holdingTimeBox
            // 
            this.holdingTimeBox.AutoCompleteCustomSource = null;
            this.holdingTimeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.holdingTimeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.holdingTimeBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.holdingTimeBox.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.holdingTimeBox.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.holdingTimeBox.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.holdingTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.holdingTimeBox.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.holdingTimeBox.Image = null;
            this.holdingTimeBox.IsDerivedStyle = true;
            this.holdingTimeBox.Lines = null;
            this.holdingTimeBox.Location = new System.Drawing.Point(329, 78);
            this.holdingTimeBox.MaxLength = 32767;
            this.holdingTimeBox.Multiline = false;
            this.holdingTimeBox.Name = "holdingTimeBox";
            this.holdingTimeBox.ReadOnly = false;
            this.holdingTimeBox.Size = new System.Drawing.Size(73, 30);
            this.holdingTimeBox.Style = MetroSet_UI.Enums.Style.Light;
            this.holdingTimeBox.StyleManager = styleManager1;
            this.holdingTimeBox.TabIndex = 31;
            this.holdingTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.holdingTimeBox.ThemeAuthor = null;
            this.holdingTimeBox.ThemeName = null;
            this.holdingTimeBox.UseSystemPasswordChar = false;
            this.holdingTimeBox.WatermarkText = "";
            // 
            // metroSetLabel47
            // 
            this.metroSetLabel47.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel47.IsDerivedStyle = true;
            this.metroSetLabel47.Location = new System.Drawing.Point(326, 54);
            this.metroSetLabel47.Name = "metroSetLabel47";
            this.metroSetLabel47.Size = new System.Drawing.Size(84, 23);
            this.metroSetLabel47.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel47.StyleManager = styleManager1;
            this.metroSetLabel47.TabIndex = 30;
            this.metroSetLabel47.Text = "Reação/Ms";
            this.metroSetLabel47.ThemeAuthor = null;
            this.metroSetLabel47.ThemeName = null;
            // 
            // updateRecoil
            // 
            this.updateRecoil.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateRecoil.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateRecoil.DisabledForeColor = System.Drawing.Color.Gray;
            this.updateRecoil.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.updateRecoil.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.updateRecoil.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.updateRecoil.HoverTextColor = System.Drawing.Color.White;
            this.updateRecoil.IsDerivedStyle = true;
            this.updateRecoil.Location = new System.Drawing.Point(317, 143);
            this.updateRecoil.Name = "updateRecoil";
            this.updateRecoil.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateRecoil.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.updateRecoil.NormalTextColor = System.Drawing.Color.White;
            this.updateRecoil.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.updateRecoil.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.updateRecoil.PressTextColor = System.Drawing.Color.White;
            this.updateRecoil.Size = new System.Drawing.Size(93, 32);
            this.updateRecoil.Style = MetroSet_UI.Enums.Style.Light;
            this.updateRecoil.StyleManager = styleManager1;
            this.updateRecoil.TabIndex = 27;
            this.updateRecoil.Text = "Atualizar";
            this.updateRecoil.ThemeAuthor = null;
            this.updateRecoil.ThemeName = null;
            this.updateRecoil.Click += new System.EventHandler(this.updateRecoil_Click);
            // 
            // maxRecoilField
            // 
            this.maxRecoilField.AutoCompleteCustomSource = null;
            this.maxRecoilField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.maxRecoilField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.maxRecoilField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.maxRecoilField.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.maxRecoilField.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.maxRecoilField.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.maxRecoilField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.maxRecoilField.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.maxRecoilField.Image = null;
            this.maxRecoilField.IsDerivedStyle = true;
            this.maxRecoilField.Lines = null;
            this.maxRecoilField.Location = new System.Drawing.Point(221, 78);
            this.maxRecoilField.MaxLength = 3;
            this.maxRecoilField.Multiline = false;
            this.maxRecoilField.Name = "maxRecoilField";
            this.maxRecoilField.ReadOnly = false;
            this.maxRecoilField.Size = new System.Drawing.Size(73, 30);
            this.maxRecoilField.Style = MetroSet_UI.Enums.Style.Light;
            this.maxRecoilField.StyleManager = styleManager1;
            this.maxRecoilField.TabIndex = 26;
            this.maxRecoilField.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.maxRecoilField.ThemeAuthor = null;
            this.maxRecoilField.ThemeName = null;
            this.maxRecoilField.UseSystemPasswordChar = false;
            this.maxRecoilField.WatermarkText = "";
            // 
            // recoilMultiplierField
            // 
            this.recoilMultiplierField.AutoCompleteCustomSource = null;
            this.recoilMultiplierField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.recoilMultiplierField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.recoilMultiplierField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.recoilMultiplierField.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.recoilMultiplierField.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.recoilMultiplierField.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.recoilMultiplierField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.recoilMultiplierField.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.recoilMultiplierField.Image = null;
            this.recoilMultiplierField.IsDerivedStyle = true;
            this.recoilMultiplierField.Lines = null;
            this.recoilMultiplierField.Location = new System.Drawing.Point(114, 78);
            this.recoilMultiplierField.MaxLength = 32767;
            this.recoilMultiplierField.Multiline = false;
            this.recoilMultiplierField.Name = "recoilMultiplierField";
            this.recoilMultiplierField.ReadOnly = false;
            this.recoilMultiplierField.Size = new System.Drawing.Size(73, 30);
            this.recoilMultiplierField.Style = MetroSet_UI.Enums.Style.Light;
            this.recoilMultiplierField.StyleManager = styleManager1;
            this.recoilMultiplierField.TabIndex = 25;
            this.recoilMultiplierField.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.recoilMultiplierField.ThemeAuthor = null;
            this.recoilMultiplierField.ThemeName = null;
            this.recoilMultiplierField.UseSystemPasswordChar = false;
            this.recoilMultiplierField.WatermarkText = "";
            // 
            // metroSetLabel6
            // 
            this.metroSetLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel6.IsDerivedStyle = true;
            this.metroSetLabel6.Location = new System.Drawing.Point(235, 54);
            this.metroSetLabel6.Name = "metroSetLabel6";
            this.metroSetLabel6.Size = new System.Drawing.Size(44, 23);
            this.metroSetLabel6.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel6.StyleManager = styleManager1;
            this.metroSetLabel6.TabIndex = 24;
            this.metroSetLabel6.Text = "Steps";
            this.metroSetLabel6.ThemeAuthor = null;
            this.metroSetLabel6.ThemeName = null;
            // 
            // metroSetLabel17
            // 
            this.metroSetLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel17.IsDerivedStyle = true;
            this.metroSetLabel17.Location = new System.Drawing.Point(109, 54);
            this.metroSetLabel17.Name = "metroSetLabel17";
            this.metroSetLabel17.Size = new System.Drawing.Size(90, 23);
            this.metroSetLabel17.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel17.StyleManager = styleManager1;
            this.metroSetLabel17.TabIndex = 23;
            this.metroSetLabel17.Text = "Multiplicação";
            this.metroSetLabel17.ThemeAuthor = null;
            this.metroSetLabel17.ThemeName = null;
            // 
            // metroSetLabel3
            // 
            this.metroSetLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel3.IsDerivedStyle = true;
            this.metroSetLabel3.Location = new System.Drawing.Point(66, 8);
            this.metroSetLabel3.Name = "metroSetLabel3";
            this.metroSetLabel3.Size = new System.Drawing.Size(100, 23);
            this.metroSetLabel3.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel3.StyleManager = styleManager1;
            this.metroSetLabel3.TabIndex = 9;
            this.metroSetLabel3.Text = "No Recoil";
            this.metroSetLabel3.ThemeAuthor = null;
            this.metroSetLabel3.ThemeName = null;
            // 
            // metroSetSwitch3
            // 
            this.metroSetSwitch3.BackColor = System.Drawing.Color.Transparent;
            this.metroSetSwitch3.BackgroundColor = System.Drawing.Color.Empty;
            this.metroSetSwitch3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(159)))), ((int)(((byte)(147)))));
            this.metroSetSwitch3.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetSwitch3.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.metroSetSwitch3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroSetSwitch3.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.metroSetSwitch3.DisabledCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetSwitch3.DisabledUnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.metroSetSwitch3.IsDerivedStyle = true;
            this.metroSetSwitch3.Location = new System.Drawing.Point(2, 7);
            this.metroSetSwitch3.Name = "metroSetSwitch3";
            this.metroSetSwitch3.Size = new System.Drawing.Size(58, 22);
            this.metroSetSwitch3.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetSwitch3.StyleManager = styleManager1;
            this.metroSetSwitch3.Switched = false;
            this.metroSetSwitch3.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.metroSetSwitch3.TabIndex = 8;
            this.metroSetSwitch3.Text = "AimbotSwitch";
            this.metroSetSwitch3.ThemeAuthor = null;
            this.metroSetSwitch3.ThemeName = null;
            this.metroSetSwitch3.UnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.metroSetSwitch3.SwitchedChanged += new MetroSet_UI.Controls.MetroSetSwitch.SwitchedChangedEventHandler(this.metroSetSwitch3_SwitchedChanged);
            // 
            // metroSetDivider1
            // 
            this.metroSetDivider1.IsDerivedStyle = true;
            this.metroSetDivider1.Location = new System.Drawing.Point(2, 34);
            this.metroSetDivider1.Name = "metroSetDivider1";
            this.metroSetDivider1.Orientation = MetroSet_UI.Enums.DividerStyle.Horizontal;
            this.metroSetDivider1.Size = new System.Drawing.Size(416, 4);
            this.metroSetDivider1.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetDivider1.StyleManager = styleManager1;
            this.metroSetDivider1.TabIndex = 7;
            this.metroSetDivider1.Text = "metroSetDivider1";
            this.metroSetDivider1.ThemeAuthor = null;
            this.metroSetDivider1.ThemeName = null;
            this.metroSetDivider1.Thickness = 1;
            // 
            // triggerTab
            // 
            this.triggerTab.BaseColor = System.Drawing.Color.White;
            this.triggerTab.Controls.Add(this.AppleFOVY);
            this.triggerTab.Controls.Add(this.AppleFOVX);
            this.triggerTab.Controls.Add(this.metroSetLabel36);
            this.triggerTab.Controls.Add(this.metroSetLabel37);
            this.triggerTab.Controls.Add(this.metroSetButton1);
            this.triggerTab.Controls.Add(this.metroSetComboBox1);
            this.triggerTab.Controls.Add(this.metroSetLabel35);
            this.triggerTab.Controls.Add(this.triggerDelay);
            this.triggerTab.Controls.Add(this.metroSetLabel34);
            this.triggerTab.Controls.Add(this.metroSetLabel33);
            this.triggerTab.Controls.Add(this.AppleSwitch);
            this.triggerTab.Controls.Add(this.metroSetDivider4);
            this.triggerTab.Font = null;
            this.triggerTab.ImageIndex = 0;
            this.triggerTab.ImageKey = null;
            this.triggerTab.IsDerivedStyle = true;
            this.triggerTab.Location = new System.Drawing.Point(4, 42);
            this.triggerTab.Name = "triggerTab";
            this.triggerTab.Size = new System.Drawing.Size(424, 300);
            this.triggerTab.Style = MetroSet_UI.Enums.Style.Light;
            this.triggerTab.StyleManager = styleManager1;
            this.triggerTab.TabIndex = 7;
            this.triggerTab.Text = "Trigger";
            this.triggerTab.ThemeAuthor = "Narwin";
            this.triggerTab.ThemeName = "MetroLite";
            this.triggerTab.ToolTipText = null;
            this.triggerTab.Click += new System.EventHandler(this.triggerTab_Click);
            // 
            // AppleFOVY
            // 
            this.AppleFOVY.AutoCompleteCustomSource = null;
            this.AppleFOVY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.AppleFOVY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.AppleFOVY.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.AppleFOVY.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.AppleFOVY.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.AppleFOVY.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.AppleFOVY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.AppleFOVY.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.AppleFOVY.Image = null;
            this.AppleFOVY.IsDerivedStyle = true;
            this.AppleFOVY.Lines = null;
            this.AppleFOVY.Location = new System.Drawing.Point(281, 84);
            this.AppleFOVY.MaxLength = 3;
            this.AppleFOVY.Multiline = false;
            this.AppleFOVY.Name = "AppleFOVY";
            this.AppleFOVY.ReadOnly = false;
            this.AppleFOVY.Size = new System.Drawing.Size(129, 30);
            this.AppleFOVY.Style = MetroSet_UI.Enums.Style.Light;
            this.AppleFOVY.StyleManager = styleManager1;
            this.AppleFOVY.TabIndex = 52;
            this.AppleFOVY.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AppleFOVY.ThemeAuthor = null;
            this.AppleFOVY.ThemeName = null;
            this.AppleFOVY.UseSystemPasswordChar = false;
            this.AppleFOVY.WatermarkText = "";
            // 
            // AppleFOVX
            // 
            this.AppleFOVX.AutoCompleteCustomSource = null;
            this.AppleFOVX.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.AppleFOVX.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.AppleFOVX.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.AppleFOVX.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.AppleFOVX.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.AppleFOVX.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.AppleFOVX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.AppleFOVX.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.AppleFOVX.Image = null;
            this.AppleFOVX.IsDerivedStyle = true;
            this.AppleFOVX.Lines = null;
            this.AppleFOVX.Location = new System.Drawing.Point(137, 84);
            this.AppleFOVX.MaxLength = 3;
            this.AppleFOVX.Multiline = false;
            this.AppleFOVX.Name = "AppleFOVX";
            this.AppleFOVX.ReadOnly = false;
            this.AppleFOVX.Size = new System.Drawing.Size(129, 30);
            this.AppleFOVX.Style = MetroSet_UI.Enums.Style.Light;
            this.AppleFOVX.StyleManager = styleManager1;
            this.AppleFOVX.TabIndex = 51;
            this.AppleFOVX.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AppleFOVX.ThemeAuthor = null;
            this.AppleFOVX.ThemeName = null;
            this.AppleFOVX.UseSystemPasswordChar = false;
            this.AppleFOVX.WatermarkText = "";
            // 
            // metroSetLabel36
            // 
            this.metroSetLabel36.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel36.IsDerivedStyle = true;
            this.metroSetLabel36.Location = new System.Drawing.Point(291, 58);
            this.metroSetLabel36.Name = "metroSetLabel36";
            this.metroSetLabel36.Size = new System.Drawing.Size(102, 23);
            this.metroSetLabel36.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel36.StyleManager = styleManager1;
            this.metroSetLabel36.TabIndex = 50;
            this.metroSetLabel36.Text = "Trigger FOV Y";
            this.metroSetLabel36.ThemeAuthor = null;
            this.metroSetLabel36.ThemeName = null;
            // 
            // metroSetLabel37
            // 
            this.metroSetLabel37.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel37.IsDerivedStyle = true;
            this.metroSetLabel37.Location = new System.Drawing.Point(152, 58);
            this.metroSetLabel37.Name = "metroSetLabel37";
            this.metroSetLabel37.Size = new System.Drawing.Size(101, 23);
            this.metroSetLabel37.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel37.StyleManager = styleManager1;
            this.metroSetLabel37.TabIndex = 49;
            this.metroSetLabel37.Text = "Trigger FOV X";
            this.metroSetLabel37.ThemeAuthor = null;
            this.metroSetLabel37.ThemeName = null;
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetButton1.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetButton1.DisabledForeColor = System.Drawing.Color.Gray;
            this.metroSetButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroSetButton1.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.metroSetButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.metroSetButton1.HoverTextColor = System.Drawing.Color.White;
            this.metroSetButton1.IsDerivedStyle = true;
            this.metroSetButton1.Location = new System.Drawing.Point(317, 143);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetButton1.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetButton1.NormalTextColor = System.Drawing.Color.White;
            this.metroSetButton1.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.metroSetButton1.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.metroSetButton1.PressTextColor = System.Drawing.Color.White;
            this.metroSetButton1.Size = new System.Drawing.Size(93, 32);
            this.metroSetButton1.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetButton1.StyleManager = styleManager1;
            this.metroSetButton1.TabIndex = 48;
            this.metroSetButton1.Text = "Atualizar";
            this.metroSetButton1.ThemeAuthor = null;
            this.metroSetButton1.ThemeName = null;
            this.metroSetButton1.Click += new System.EventHandler(this.metroSetButton1_Click);
            // 
            // metroSetComboBox1
            // 
            this.metroSetComboBox1.AllowDrop = true;
            this.metroSetComboBox1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.metroSetComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetComboBox1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroSetComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.metroSetComboBox1.CausesValidation = false;
            this.metroSetComboBox1.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.metroSetComboBox1.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.metroSetComboBox1.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.metroSetComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.metroSetComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.metroSetComboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.metroSetComboBox1.FormattingEnabled = true;
            this.metroSetComboBox1.IsDerivedStyle = true;
            this.metroSetComboBox1.ItemHeight = 20;
            this.metroSetComboBox1.Items.AddRange(new object[] {
            "Mouse 1",
            "Mouse 2",
            "Mouse 3",
            "Mouse 4",
            "Mouse 5",
            "Shift",
            "Alt",
            "CTRL",
            "Caps Lock"});
            this.metroSetComboBox1.Location = new System.Drawing.Point(295, 3);
            this.metroSetComboBox1.Name = "metroSetComboBox1";
            this.metroSetComboBox1.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetComboBox1.SelectedItemForeColor = System.Drawing.Color.White;
            this.metroSetComboBox1.Size = new System.Drawing.Size(121, 26);
            this.metroSetComboBox1.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetComboBox1.StyleManager = styleManager1;
            this.metroSetComboBox1.TabIndex = 45;
            this.metroSetComboBox1.ThemeAuthor = null;
            this.metroSetComboBox1.ThemeName = null;
            this.metroSetComboBox1.SelectedIndexChanged += new System.EventHandler(this.metroSetComboBox1_SelectedIndexChanged);
            // 
            // metroSetLabel35
            // 
            this.metroSetLabel35.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel35.IsDerivedStyle = true;
            this.metroSetLabel35.Location = new System.Drawing.Point(234, 6);
            this.metroSetLabel35.Name = "metroSetLabel35";
            this.metroSetLabel35.Size = new System.Drawing.Size(55, 23);
            this.metroSetLabel35.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel35.StyleManager = styleManager1;
            this.metroSetLabel35.TabIndex = 44;
            this.metroSetLabel35.Text = "HotKey";
            this.metroSetLabel35.ThemeAuthor = null;
            this.metroSetLabel35.ThemeName = null;
            // 
            // triggerDelay
            // 
            this.triggerDelay.AutoCompleteCustomSource = null;
            this.triggerDelay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.triggerDelay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.triggerDelay.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.triggerDelay.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.triggerDelay.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.triggerDelay.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.triggerDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.triggerDelay.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.triggerDelay.Image = null;
            this.triggerDelay.IsDerivedStyle = true;
            this.triggerDelay.Lines = null;
            this.triggerDelay.Location = new System.Drawing.Point(8, 84);
            this.triggerDelay.MaxLength = 3;
            this.triggerDelay.Multiline = false;
            this.triggerDelay.Name = "triggerDelay";
            this.triggerDelay.ReadOnly = false;
            this.triggerDelay.Size = new System.Drawing.Size(118, 30);
            this.triggerDelay.Style = MetroSet_UI.Enums.Style.Light;
            this.triggerDelay.StyleManager = styleManager1;
            this.triggerDelay.TabIndex = 43;
            this.triggerDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.triggerDelay.ThemeAuthor = null;
            this.triggerDelay.ThemeName = null;
            this.triggerDelay.UseSystemPasswordChar = false;
            this.triggerDelay.WatermarkText = "";
            // 
            // metroSetLabel34
            // 
            this.metroSetLabel34.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel34.IsDerivedStyle = true;
            this.metroSetLabel34.Location = new System.Drawing.Point(1, 58);
            this.metroSetLabel34.Name = "metroSetLabel34";
            this.metroSetLabel34.Size = new System.Drawing.Size(157, 23);
            this.metroSetLabel34.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel34.StyleManager = styleManager1;
            this.metroSetLabel34.TabIndex = 42;
            this.metroSetLabel34.Text = "Reaction Time (ms)";
            this.metroSetLabel34.ThemeAuthor = null;
            this.metroSetLabel34.ThemeName = null;
            // 
            // metroSetLabel33
            // 
            this.metroSetLabel33.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel33.IsDerivedStyle = true;
            this.metroSetLabel33.Location = new System.Drawing.Point(66, 8);
            this.metroSetLabel33.Name = "metroSetLabel33";
            this.metroSetLabel33.Size = new System.Drawing.Size(100, 23);
            this.metroSetLabel33.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel33.StyleManager = styleManager1;
            this.metroSetLabel33.TabIndex = 15;
            this.metroSetLabel33.Text = "Trigger Bot";
            this.metroSetLabel33.ThemeAuthor = null;
            this.metroSetLabel33.ThemeName = null;
            // 
            // AppleSwitch
            // 
            this.AppleSwitch.BackColor = System.Drawing.Color.Transparent;
            this.AppleSwitch.BackgroundColor = System.Drawing.Color.Empty;
            this.AppleSwitch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(159)))), ((int)(((byte)(147)))));
            this.AppleSwitch.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.AppleSwitch.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.AppleSwitch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AppleSwitch.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.AppleSwitch.DisabledCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.AppleSwitch.DisabledUnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.AppleSwitch.IsDerivedStyle = true;
            this.AppleSwitch.Location = new System.Drawing.Point(2, 7);
            this.AppleSwitch.Name = "AppleSwitch";
            this.AppleSwitch.Size = new System.Drawing.Size(58, 22);
            this.AppleSwitch.Style = MetroSet_UI.Enums.Style.Light;
            this.AppleSwitch.StyleManager = styleManager1;
            this.AppleSwitch.Switched = false;
            this.AppleSwitch.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.AppleSwitch.TabIndex = 14;
            this.AppleSwitch.Text = "metroSetSwitch6";
            this.AppleSwitch.ThemeAuthor = null;
            this.AppleSwitch.ThemeName = null;
            this.AppleSwitch.UnCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.AppleSwitch.SwitchedChanged += new MetroSet_UI.Controls.MetroSetSwitch.SwitchedChangedEventHandler(this.AppleSwitch_SwitchedChanged);
            // 
            // metroSetDivider4
            // 
            this.metroSetDivider4.IsDerivedStyle = true;
            this.metroSetDivider4.Location = new System.Drawing.Point(2, 34);
            this.metroSetDivider4.Name = "metroSetDivider4";
            this.metroSetDivider4.Orientation = MetroSet_UI.Enums.DividerStyle.Horizontal;
            this.metroSetDivider4.Size = new System.Drawing.Size(416, 4);
            this.metroSetDivider4.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetDivider4.StyleManager = styleManager1;
            this.metroSetDivider4.TabIndex = 13;
            this.metroSetDivider4.Text = "metroSetDivider4";
            this.metroSetDivider4.ThemeAuthor = null;
            this.metroSetDivider4.ThemeName = null;
            this.metroSetDivider4.Thickness = 1;
            // 
            // metroSetSetTabPage2
            // 
            this.metroSetSetTabPage2.BaseColor = System.Drawing.Color.White;
            this.metroSetSetTabPage2.Controls.Add(this.metroSetLabel7);
            this.metroSetSetTabPage2.Controls.Add(this.metroSetComboBox2);
            this.metroSetSetTabPage2.Controls.Add(this.UpdateRes);
            this.metroSetSetTabPage2.Controls.Add(this.resolutionYField);
            this.metroSetSetTabPage2.Controls.Add(this.resolutionXField);
            this.metroSetSetTabPage2.Controls.Add(this.resolutionX);
            this.metroSetSetTabPage2.Font = null;
            this.metroSetSetTabPage2.ImageIndex = 0;
            this.metroSetSetTabPage2.ImageKey = null;
            this.metroSetSetTabPage2.IsDerivedStyle = true;
            this.metroSetSetTabPage2.Location = new System.Drawing.Point(4, 42);
            this.metroSetSetTabPage2.Name = "metroSetSetTabPage2";
            this.metroSetSetTabPage2.Size = new System.Drawing.Size(424, 300);
            this.metroSetSetTabPage2.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetSetTabPage2.StyleManager = styleManager1;
            this.metroSetSetTabPage2.TabIndex = 3;
            this.metroSetSetTabPage2.Text = "Settings";
            this.metroSetSetTabPage2.ThemeAuthor = "Narwin";
            this.metroSetSetTabPage2.ThemeName = "MetroLite";
            this.metroSetSetTabPage2.ToolTipText = null;
            // 
            // metroSetLabel7
            // 
            this.metroSetLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetLabel7.IsDerivedStyle = true;
            this.metroSetLabel7.Location = new System.Drawing.Point(167, 18);
            this.metroSetLabel7.Name = "metroSetLabel7";
            this.metroSetLabel7.Size = new System.Drawing.Size(91, 23);
            this.metroSetLabel7.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetLabel7.StyleManager = styleManager1;
            this.metroSetLabel7.TabIndex = 16;
            this.metroSetLabel7.Text = "Monitor Atual";
            this.metroSetLabel7.ThemeAuthor = null;
            this.metroSetLabel7.ThemeName = null;
            // 
            // metroSetComboBox2
            // 
            this.metroSetComboBox2.AllowDrop = true;
            this.metroSetComboBox2.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.metroSetComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.metroSetComboBox2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroSetComboBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.metroSetComboBox2.CausesValidation = false;
            this.metroSetComboBox2.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.metroSetComboBox2.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.metroSetComboBox2.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.metroSetComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.metroSetComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.metroSetComboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.metroSetComboBox2.FormattingEnabled = true;
            this.metroSetComboBox2.IsDerivedStyle = true;
            this.metroSetComboBox2.ItemHeight = 20;
            this.metroSetComboBox2.Location = new System.Drawing.Point(154, 43);
            this.metroSetComboBox2.Name = "metroSetComboBox2";
            this.metroSetComboBox2.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetComboBox2.SelectedItemForeColor = System.Drawing.Color.White;
            this.metroSetComboBox2.Size = new System.Drawing.Size(121, 26);
            this.metroSetComboBox2.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetComboBox2.StyleManager = styleManager1;
            this.metroSetComboBox2.TabIndex = 15;
            this.metroSetComboBox2.ThemeAuthor = null;
            this.metroSetComboBox2.ThemeName = null;
            this.metroSetComboBox2.SelectedIndexChanged += new System.EventHandler(this.metroSetComboBox2_SelectedIndexChanged);
            // 
            // UpdateRes
            // 
            this.UpdateRes.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.UpdateRes.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.UpdateRes.DisabledForeColor = System.Drawing.Color.Gray;
            this.UpdateRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateRes.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.UpdateRes.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.UpdateRes.HoverTextColor = System.Drawing.Color.White;
            this.UpdateRes.IsDerivedStyle = true;
            this.UpdateRes.Location = new System.Drawing.Point(317, 143);
            this.UpdateRes.Name = "UpdateRes";
            this.UpdateRes.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.UpdateRes.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.UpdateRes.NormalTextColor = System.Drawing.Color.White;
            this.UpdateRes.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.UpdateRes.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.UpdateRes.PressTextColor = System.Drawing.Color.White;
            this.UpdateRes.Size = new System.Drawing.Size(93, 32);
            this.UpdateRes.Style = MetroSet_UI.Enums.Style.Light;
            this.UpdateRes.StyleManager = styleManager1;
            this.UpdateRes.TabIndex = 4;
            this.UpdateRes.Text = "Atualizar";
            this.UpdateRes.ThemeAuthor = null;
            this.UpdateRes.ThemeName = null;
            this.UpdateRes.Click += new System.EventHandler(this.UpdateRes_Click);
            // 
            // resolutionYField
            // 
            this.resolutionYField.AutoCompleteCustomSource = null;
            this.resolutionYField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.resolutionYField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.resolutionYField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.resolutionYField.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.resolutionYField.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.resolutionYField.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.resolutionYField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.resolutionYField.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.resolutionYField.Image = null;
            this.resolutionYField.IsDerivedStyle = true;
            this.resolutionYField.Lines = null;
            this.resolutionYField.Location = new System.Drawing.Point(73, 40);
            this.resolutionYField.MaxLength = 4;
            this.resolutionYField.Multiline = false;
            this.resolutionYField.Name = "resolutionYField";
            this.resolutionYField.ReadOnly = false;
            this.resolutionYField.Size = new System.Drawing.Size(61, 30);
            this.resolutionYField.Style = MetroSet_UI.Enums.Style.Light;
            this.resolutionYField.StyleManager = styleManager1;
            this.resolutionYField.TabIndex = 3;
            this.resolutionYField.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.resolutionYField.ThemeAuthor = null;
            this.resolutionYField.ThemeName = null;
            this.resolutionYField.UseSystemPasswordChar = false;
            this.resolutionYField.WatermarkText = "";
            // 
            // resolutionXField
            // 
            this.resolutionXField.AutoCompleteCustomSource = null;
            this.resolutionXField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.resolutionXField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.resolutionXField.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.resolutionXField.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.resolutionXField.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.resolutionXField.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.resolutionXField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.resolutionXField.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.resolutionXField.Image = null;
            this.resolutionXField.IsDerivedStyle = true;
            this.resolutionXField.Lines = null;
            this.resolutionXField.Location = new System.Drawing.Point(8, 40);
            this.resolutionXField.MaxLength = 4;
            this.resolutionXField.Multiline = false;
            this.resolutionXField.Name = "resolutionXField";
            this.resolutionXField.ReadOnly = false;
            this.resolutionXField.Size = new System.Drawing.Size(61, 30);
            this.resolutionXField.Style = MetroSet_UI.Enums.Style.Light;
            this.resolutionXField.StyleManager = styleManager1;
            this.resolutionXField.TabIndex = 2;
            this.resolutionXField.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.resolutionXField.ThemeAuthor = null;
            this.resolutionXField.ThemeName = null;
            this.resolutionXField.UseSystemPasswordChar = false;
            this.resolutionXField.WatermarkText = "";
            // 
            // resolutionX
            // 
            this.resolutionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.resolutionX.IsDerivedStyle = true;
            this.resolutionX.Location = new System.Drawing.Point(5, 15);
            this.resolutionX.Name = "resolutionX";
            this.resolutionX.Size = new System.Drawing.Size(135, 23);
            this.resolutionX.Style = MetroSet_UI.Enums.Style.Light;
            this.resolutionX.StyleManager = styleManager1;
            this.resolutionX.TabIndex = 0;
            this.resolutionX.Text = "Resolução do Jogo";
            this.resolutionX.ThemeAuthor = null;
            this.resolutionX.ThemeName = null;
            // 
            // leonardoPorts
            // 
            this.leonardoPorts.AllowDrop = true;
            this.leonardoPorts.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.leonardoPorts.BackColor = System.Drawing.Color.Transparent;
            this.leonardoPorts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.leonardoPorts.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.leonardoPorts.CausesValidation = false;
            this.leonardoPorts.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.leonardoPorts.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.leonardoPorts.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.leonardoPorts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.leonardoPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leonardoPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.leonardoPorts.FormattingEnabled = true;
            this.leonardoPorts.IsDerivedStyle = true;
            this.leonardoPorts.ItemHeight = 20;
            this.leonardoPorts.Location = new System.Drawing.Point(19, 6);
            this.leonardoPorts.Name = "leonardoPorts";
            this.leonardoPorts.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.leonardoPorts.SelectedItemForeColor = System.Drawing.Color.White;
            this.leonardoPorts.Size = new System.Drawing.Size(121, 26);
            this.leonardoPorts.Style = MetroSet_UI.Enums.Style.Light;
            this.leonardoPorts.StyleManager = styleManager1;
            this.leonardoPorts.TabIndex = 11;
            this.leonardoPorts.ThemeAuthor = null;
            this.leonardoPorts.ThemeName = null;
            this.leonardoPorts.SelectedIndexChanged += new System.EventHandler(this.leonardoPorts_SelectedIndexChanged);
            // 
            // currentModeCombo
            // 
            this.currentModeCombo.AllowDrop = true;
            this.currentModeCombo.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.currentModeCombo.BackColor = System.Drawing.Color.Transparent;
            this.currentModeCombo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.currentModeCombo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.currentModeCombo.CausesValidation = false;
            this.currentModeCombo.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.currentModeCombo.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.currentModeCombo.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.currentModeCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.currentModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currentModeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.currentModeCombo.FormattingEnabled = true;
            this.currentModeCombo.IsDerivedStyle = true;
            this.currentModeCombo.ItemHeight = 20;
            this.currentModeCombo.Items.AddRange(new object[] {
            "Mode 1 (F1)",
            "Mode 2 (F2)",
            "Mode 3 (F3)"});
            this.currentModeCombo.Location = new System.Drawing.Point(146, 6);
            this.currentModeCombo.Name = "currentModeCombo";
            this.currentModeCombo.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.currentModeCombo.SelectedItemForeColor = System.Drawing.Color.White;
            this.currentModeCombo.Size = new System.Drawing.Size(97, 26);
            this.currentModeCombo.Style = MetroSet_UI.Enums.Style.Light;
            this.currentModeCombo.StyleManager = styleManager1;
            this.currentModeCombo.TabIndex = 31;
            this.currentModeCombo.ThemeAuthor = null;
            this.currentModeCombo.ThemeName = null;
            this.currentModeCombo.SelectedIndexChanged += new System.EventHandler(this.currentModeCombo_SelectedIndexChanged);
            // 
            // metroSetSetToolTip1
            // 
            this.metroSetSetToolTip1.BackColor = System.Drawing.Color.White;
            this.metroSetSetToolTip1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.metroSetSetToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.metroSetSetToolTip1.IsDerivedStyle = true;
            this.metroSetSetToolTip1.OwnerDraw = true;
            this.metroSetSetToolTip1.Style = MetroSet_UI.Enums.Style.Light;
            this.metroSetSetToolTip1.StyleManager = null;
            this.metroSetSetToolTip1.ThemeAuthor = "Narwin";
            this.metroSetSetToolTip1.ThemeName = "MetroLite";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Double click on me and you see me again :-)!";
            this.notifyIcon1.BalloonTipTitle = "I\'m hereee";
            this.notifyIcon1.Text = "Thats Unknown";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // closeButton
            // 
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.closeButton.IsDerivedStyle = true;
            this.closeButton.Location = new System.Drawing.Point(434, 7);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(18, 23);
            this.closeButton.Style = MetroSet_UI.Enums.Style.Light;
            this.closeButton.StyleManager = styleManager1;
            this.closeButton.TabIndex = 32;
            this.closeButton.Text = "X";
            this.closeButton.ThemeAuthor = null;
            this.closeButton.ThemeName = null;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 450);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.currentModeCombo);
            this.Controls.Add(this.leonardoPorts);
            this.Controls.Add(this.SystemTabs);
            this.Enabled = false;
            this.MaximumSize = new System.Drawing.Size(455, 450);
            this.MinimumSize = new System.Drawing.Size(455, 450);
            this.Name = "MainScreen";
            this.StyleManager = styleManager1;
            this.Text = "Zeus Build ";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainScreen_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Move += new System.EventHandler(this.MainScreen_Move);
            this.Resize += new System.EventHandler(this.MainScreen_Resize);
            this.SystemTabs.ResumeLayout(false);
            this.AimbotTab.ResumeLayout(false);
            this.noRCSTab.ResumeLayout(false);
            this.triggerTab.ResumeLayout(false);
            this.metroSetSetTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

	}

	static MainScreen()
	{
		_debug = false;
		_debugWindow = null;
		_leonardo = null;
		consoleName = "Martins Compiled Build";
	}

    private void SystemTabs_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void jitter_Click(object sender, EventArgs e)
    {

    }

    private void bhaSpeed_Click(object sender, EventArgs e)
    {

    }

    private void ingameSens_Click(object sender, EventArgs e)
    {
    }

    private void ingameSens_TextChanged(object sender, EventArgs e)
    {
        if (ingameSens.Text.Length > 0)
        {
            for (int i = 0; i < ingameSens.Text.Length; i++)
            {
                if (!(ingameSens.Text[i] == '1' || ingameSens.Text[i] == '2' || ingameSens.Text[i] == '3' || ingameSens.Text[i] == '4' || ingameSens.Text[i] == '5' || ingameSens.Text[i] == '6' || ingameSens.Text[i] == '7' || ingameSens.Text[i] == '8' || ingameSens.Text[i] == '9' || ingameSens.Text[i] == '!' || ingameSens.Text[i] == '.'))
                {
                    if (ingameSens.Text[i] == ',')
                    {
                        ingameSens.Text = ingameSens.Text.Replace(ingameSens.Text[i], '.');
                    }
                    else
                    {
                        ingameSens.Text = ingameSens.Text.Remove(i, 1);
                    }
                    i--;
                }
            }
        }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void triggerTab_Click(object sender, EventArgs e)
    {

    }
}
