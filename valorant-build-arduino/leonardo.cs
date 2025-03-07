// unknown.leonardo
using System;
using System.IO.Ports;
using unknown;

public class leonardo
{
	private SerialPort port = null;

	public bool leonardoOpened = false;

	public leonardo(string COMPORT)
	{
		port = new SerialPort(COMPORT, 115100);
		try
		{
			port.Open();
			leonardoOpened = true;
		}
		catch (Exception ex)
		{
			leonardoOpened = false;
		}
	}

	public void Move(int x, int y, bool shot)
	{
		if (port != null)
		{
			port.Write(roundtosignedchar(x) + "," + roundtosignedchar(y) + "," + Convert.ToInt32(shot));
		}
	}

	private int roundtosignedchar(int a)
	{
		if (a < -127)
		{
			return -127;
		}
		if (a > 127)
		{
			return 127;
		}
		return a;
	}
}
