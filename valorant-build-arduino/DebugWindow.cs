// unknown.DebugWindow
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroSet_UI.Enums;
using MetroSet_UI.Forms;

public class DebugWindow : MetroSetForm
{
	private IContainer components = null;

	private ListBox listBox1;

	public DebugWindow()
	{
		InitializeComponent();
	}

	private void DebugWindow_Load(object sender, EventArgs e)
	{
	}

	public void AddDebugLine(string line)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				listBox1.Items.Add(line);
			});
			Invoke((MethodInvoker)delegate
			{
				listBox1.SelectedIndex = listBox1.Items.Count - 1;
			});
			Invoke((MethodInvoker)delegate
			{
				listBox1.TopIndex = listBox1.Items.Count - 1;
			});
		}
		catch
		{
		}
	}

	public void ClearDebugConsole()
	{
		listBox1.Items.Clear();
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
		this.listBox1 = new System.Windows.Forms.ListBox();
		base.SuspendLayout();
		this.listBox1.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
		this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.listBox1.ForeColor = System.Drawing.SystemColors.ScrollBar;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.ItemHeight = 20;
		this.listBox1.Location = new System.Drawing.Point(5, 65);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(305, 320);
		this.listBox1.TabIndex = 1;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
		base.ClientSize = new System.Drawing.Size(315, 409);
		base.ControlBox = false;
		base.Controls.Add(this.listBox1);
		base.Enabled = false;
		this.MaximumSize = new System.Drawing.Size(315, 409);
		this.MinimumSize = new System.Drawing.Size(315, 409);
		base.Name = "DebugWindow";
		base.Padding = new System.Windows.Forms.Padding(2, 70, 2, 2);
		base.ShowHeader = true;
		base.ShowInTaskbar = false;
		base.ShowLeftRect = false;
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Style = MetroSet_UI.Enums.Style.Dark;
		this.Text = "DEBUG";
		base.TextColor = System.Drawing.Color.White;
		base.ThemeName = "MetroDark";
		base.TopMost = true;
		base.UseSlideAnimation = true;
		base.Load += new System.EventHandler(DebugWindow_Load);
		base.ResumeLayout(false);
	}
}
