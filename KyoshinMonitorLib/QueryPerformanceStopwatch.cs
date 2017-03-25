using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// Win32のQueryPerformanceCounterのラッパクラス
	/// </summary>
	public class QueryPerformanceStopwatch
	{
		[DllImport("kernel32.dll")]
		static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
		[DllImport("kernel32.dll")]
		static extern bool QueryPerformanceFrequency(ref long lpFrequency);

		private long startCounter;

		public void Start()
		{
			QueryPerformanceCounter(ref startCounter);
		}

		public TimeSpan Elapsed
		{
			get
			{
				long stopCounter = 0;
				QueryPerformanceCounter(ref stopCounter);
				long frequency = 0;
				QueryPerformanceFrequency(ref frequency);
				return TimeSpan.FromMilliseconds((stopCounter - startCounter) * 1000.0 / frequency);
			}
		}
	}
}
